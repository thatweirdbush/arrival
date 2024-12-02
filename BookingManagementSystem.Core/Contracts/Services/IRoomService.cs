using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.Core.Contracts.Services;
public interface IRoomService
{
#nullable enable
    Task<IEnumerable<DestinationTypeSymbol>> GetAllDestinationTypeSymbolsAsync();
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<IEnumerable<Property>> GetAvailableRoomsAsync(DateTimeOffset? checkIn, DateTimeOffset? checkOut, string? destination = null, int? guests = null, int? pets = null);
    Task<List<string>> SearchLocationsToStringAsync(string query, int maxRows = 10);
    Task<GeographicName?> SearchSingleLocationAsync(string locationName);
}
