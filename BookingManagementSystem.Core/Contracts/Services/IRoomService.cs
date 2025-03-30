using BookingManagementSystem.Core.Commons.Filters;
using BookingManagementSystem.Core.Commons.Paginations;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

using System.Linq.Expressions;

namespace BookingManagementSystem.Core.Contracts.Services;
public interface IRoomService
{
    Task<IEnumerable<DestinationTypeSymbol>> GetAllDestinationTypeSymbolsAsync();
    Task<IEnumerable<Property>> GetAllPropertiesAsync(Expression<Func<Property, bool>>? filter = null);
    Task<PaginatedResult<Property>> GetAvailableRoomsAsync(PropertyFilter filter);
    Task<List<string>> SearchLocationsToStringAsync(string query, int maxRows = 10);
    Task<GeographicName?> SearchSingleLocationAsync(string locationName);
    Task ToggleFavoriteAsync(Property property);
}
