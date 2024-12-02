using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;

namespace BookingManagementSystem.Core.Services;
public class RoomService : IRoomService
{
    // Properties nessesary for UI data binding
    private readonly IRepository<DestinationTypeSymbol> _destinationTypeSymbolRepository;

    // Properties nessesary for Schedule searching
    private readonly IRepository<Property> _roomRepository;
    private readonly IRepository<Booking> _bookingRepository;

    // Properties nessesary for Geographic Names searching
    private readonly GeographicNameService _geographicNamesService;

    public RoomService(
        IRepository<Property> roomRepository,
        IRepository<Booking> bookingRepository,
        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository,
        GeographicNameService geographicNamesService)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
        _geographicNamesService = geographicNamesService;
    }

    public Task<IEnumerable<DestinationTypeSymbol>> GetAllDestinationTypeSymbolsAsync()
    {
        return _destinationTypeSymbolRepository.GetAllAsync();
    }
    public Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return _roomRepository.GetAllAsync();
    }

#nullable enable
    public async Task<IEnumerable<Property>> GetAvailableRoomsAsync(
        DateTimeOffset? checkIn,
        DateTimeOffset? checkOut,
        string? destination = null,
        int? guests = null,
        int? pets = null)
    {
        // Get all bookings from the database
        var bookings = await _bookingRepository.GetAllAsync();

        // Get all bookings that overlap with the requested dates
        var unavailableRoomIds = bookings
            .Where(b => b.CheckInDate < checkOut && b.CheckOutDate > checkIn)
            .Select(b => b.PropertyId)
            .ToHashSet();

        // Get all rooms from the database
        var rooms = await _roomRepository.GetAllAsync();

        // Parse destination string into state/province and country
        string? stateOrProvince = null;
        string? countryName = null;
        destination = destination?.Trim();

        if (!string.IsNullOrEmpty(destination))
        {
            // Search for the location detailed info from API
            var location = await SearchSingleLocationAsync(destination);
            if (location != null)
            {
                stateOrProvince = location.Name;
                countryName = location.CountryName;
            }
        }

        // Parse the number of guests and pets
        var totalGuests = guests ?? 0;
        var totalPets = pets ?? 0;

        // Search for rooms that match the criteria
        List<Property> searchResults;

        if (string.IsNullOrEmpty(destination))
        {
            // If destination is ignored, only filter rooms that are not booked
            searchResults = rooms.Where(r => !unavailableRoomIds.Contains(r.Id)).ToList();
        }
        else if (string.IsNullOrEmpty(countryName)) // No need to check stateOrProvince as it comes with countryName
        {
            // Return empty list if the country is not found
            return [];
        }
        else
        {
            // Primary search: By destination (state/province and country)
            searchResults = rooms.Where(r =>
                r.Country.CountryName.Equals(countryName, StringComparison.OrdinalIgnoreCase) &&
                r.StateOrProvince.Equals(stateOrProvince, StringComparison.OrdinalIgnoreCase)).ToList();

            // Fallback: Search more by country only if results are fewer than 10
            if (searchResults.Count < 10)
            {
                var countryResults = rooms.Where(r =>
                    r.Country.CountryName.Equals(countryName, StringComparison.OrdinalIgnoreCase)).ToList();

                searchResults.AddRange(countryResults.Except(searchResults));
            }

            // Remove booked rooms
            searchResults = searchResults.Where(r => !unavailableRoomIds.Contains(r.Id)).ToList();
        }

        // Filter by number of guests and pets
        searchResults = searchResults.Where(r =>
            r.MaxGuests >= totalGuests &&
            (totalPets == 0 || r.IsPetFriendly)).ToList();

        return searchResults;
    }

    public async Task<List<string>> SearchLocationsToStringAsync(string query, int maxRows = 10)
    {
         return await _geographicNamesService.GeographicNameToStringListAsync(query, maxRows);
    }

    public async Task<GeographicName?> SearchSingleLocationAsync(string locationName)
    {
        var locations = await _geographicNamesService.SearchLocationsAsync(locationName);
        return locations.FirstOrDefault();
    }


}
