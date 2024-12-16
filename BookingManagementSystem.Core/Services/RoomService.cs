using System.Linq.Expressions;
using BookingManagementSystem.Core.Commons.Filters;
using BookingManagementSystem.Core.Commons.Paginations;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Services;
#nullable enable
public class RoomService : IRoomService
{
    // Properties nessesary for UI data binding
    private readonly DestinationTypeSymbolRepository _destinationTypeSymbolRepository;

    // Properties nessesary for Schedule searching
    private readonly IRepository<Property> _roomRepository;
    private readonly IRepository<Booking> _bookingRepository;

    // Properties nessesary for Geographic Names searching
    private readonly GeographicNameService _geographicNamesService;

    public RoomService(
        IRepository<Property> roomRepository,
        IRepository<Booking> bookingRepository,
        DestinationTypeSymbolRepository destinationTypeSymbolRepository,
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

    public Task<IEnumerable<Property>> GetAllPropertiesAsync(Expression<Func<Property, bool>>? filter = null)
    {
        return _roomRepository.GetAllAsync(filter);
    }

    public async Task<PaginatedResult<Property>> GetAvailableRoomsAsync(PropertyFilter filter)
    {
        // Business Rule: Check-in and check-out dates are required
        if (filter.CheckInDate == null || filter.CheckOutDate == null)
            return new PaginatedResult<Property>();

        // Step 1: Fetch unavailable room IDs
        var unavailableRoomIds = (await _bookingRepository.GetAllAsync(b =>
            b.CheckInDate < filter.CheckOutDate && b.CheckOutDate > filter.CheckInDate))
            .Select(b => b.PropertyId)
            .ToHashSet();

        // Step 2: Handle destination asynchronously
        string? stateOrProvince = null;
        string? countryName = null;

        if (!string.IsNullOrWhiteSpace(filter.Destination))
        {
            var location = await SearchSingleLocationAsync(filter.Destination.Trim());
            if (location != null)
            {
                stateOrProvince = location.Name?.ToLower();
                countryName = location.CountryName?.ToLower();
            }
        }

        // Step 3: Build query for available rooms
        var queryBuilder = new Func<IQueryable<Property>, IQueryable<Property>>(query =>
        {
            // Filter by listed status
            query = query.Where(r => r.Status == PropertyStatus.Listed);

            // Remove booked rooms
            query = query.Where(r => !unavailableRoomIds.Contains(r.Id));

            // Filter by destination
            if (!string.IsNullOrEmpty(countryName))
            {
                query = query.Where(r =>
                    r.Country.CountryName.ToLower().Contains(countryName) ||
                    (!string.IsNullOrEmpty(stateOrProvince) && r.StateOrProvince.ToLower().Contains(stateOrProvince)));
            }

            // Filter by guests and pets
            if (filter.MinGuests.HasValue)
                query = query.Where(r => r.MaxGuests >= filter.MinGuests.Value);

            if (filter.PetsAllowed.HasValue && filter.PetsAllowed.Value > 0)
                query = query.Where(r => r.IsPetFriendly);

            // Filter by DestinationType
            if (filter.DestinationType.HasValue)
            {
                if (filter.DestinationType == DestinationType.All)
                {
                    // Do nothing, as all properties will be returned
                }
                else if (filter.DestinationType == DestinationType.Trending)
                {
                    query = query.Where(p => p.IsPriority || p.IsFavourite);
                }
                else
                {
                    query = query.Where(r => r.DestinationTypes.Contains(filter.DestinationType.Value));
                }
            }

            // Include Country Info
            query = query.Include(r => r.Country);

            return query;
        });

        // Step 4: Execute query and return paginated results
        return await _roomRepository.GetPagedFilteredAndSortedAsync(
            queryBuilder: queryBuilder,
            keySelector: r => r.PricePerNight,
            sortDescending: false,
            pageNumber: filter.PageNumber,
            pageSize: filter.PageSize);
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

    public Task ToggleFavorite(Property property)
    {
        property.IsFavourite = !property.IsFavourite;
        property.UpdatedAt = DateTime.Now.ToUniversalTime();
        _roomRepository.UpdateAsync(property);
        return _roomRepository.SaveChangesAsync();
    }
}
