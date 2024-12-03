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

    public HomeFacade(
        IRepository<Property> propertyRepository,
        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository,
        IRoomService roomService)
    {
        _propertyRepository = propertyRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
        _roomService = roomService;
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

#nullable enable
    public async Task<IEnumerable<Property>> GetAvailableRoomsAsync(DateTimeOffset? checkIn, DateTimeOffset? checkOut, string? destination = null, int? guests = null, int? pets = null)
    {
        return await _roomService.GetAvailableRoomsAsync(checkIn, checkOut, destination, guests, pets);
    }
}
