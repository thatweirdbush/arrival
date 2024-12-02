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
        var bookings = await _bookingRepository.GetAllAsync();
        var unavailableRoomIds = bookings
            .Where(b => b.CheckInDate < checkOut && b.CheckOutDate > checkIn) // Check if the booking overlaps with the requested dates
            .Select(b => b.PropertyId)
            .ToHashSet();

        var rooms = await _roomRepository.GetAllAsync();

        // Use default values ​​if parameters are not passed
        var totalGuests = guests ?? 0;
        var totalPets = pets ?? 0;

        return rooms.Where(r =>
            !unavailableRoomIds.Contains(r.Id) &&
            (string.IsNullOrEmpty(destination) || r.Location.Equals(destination, StringComparison.OrdinalIgnoreCase)) &&
            r.MaxGuests >= totalGuests &&
            (!r.IsPetFriendly && totalPets == 0 || r.IsPetFriendly));
    }

    public async Task<List<string>> SearchLocationsAsync(string query, int maxRows = 10)
    {
        var data = await _geographicNamesService.SearchLocationsOriginalAsync(query, maxRows);

        // Concatenate the `Name` and `CountryName` of the locations into a list of strings
        return data.Select(location => $"{location.Name}, {location.CountryName}").ToList();
    }

}
