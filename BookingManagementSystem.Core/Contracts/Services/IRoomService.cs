using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Services;
public interface IRoomService
{
#nullable enable
    Task<IEnumerable<DestinationTypeSymbol>> GetAllDestinationTypeSymbolsAsync();
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<IEnumerable<Property>> GetAvailableRoomsAsync(DateTimeOffset? checkIn, DateTimeOffset? checkOut, string? destination = null, int? guests = null, int? pets = null);
    Task<List<string>> SearchLocationsAsync(string query, int maxRows = 10);
}
