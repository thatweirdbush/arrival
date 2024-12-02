using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.Core.Facades;
public class HomeFacade : IHomeFacade
{
    // Properties nessesary for UI data binding
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<DestinationTypeSymbol> _destinationTypeSymbolRepository;

    // Properties nessesary for Schedule searching
    private readonly IRoomService _roomService;

    // Properties nessesary for Geographic Names searching
    private readonly GeographicNameService _geographicNamesService;

    public HomeFacade(
        IRepository<Property> propertyRepository,
        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository,
        IRoomService roomService,
        GeographicNameService geographicNamesService)
    {
        _propertyRepository = propertyRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
        _roomService = roomService;
        _geographicNamesService = geographicNamesService;
    }

    public Task<IEnumerable<DestinationTypeSymbol>> GetAllDestinationTypeSymbolsAsync()
    {
        return _destinationTypeSymbolRepository.GetAllAsync();
    }
    public Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return _propertyRepository.GetAllAsync();
    }
    public async Task<IEnumerable<Property>> GetAvailableRoomsAsync(DateTimeOffset? checkIn, DateTimeOffset? checkOut)
    {
        return await _roomService.GetAvailableRoomsAsync(checkIn, checkOut);
    }

    public async Task<List<string>> SearchLocationsAsync(string query, int maxRows = 10)
    {
        return await _geographicNamesService.SearchLocationsAsync(query, maxRows);
    }
}
