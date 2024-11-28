using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Services;
public class RoomService : IRoomService
{
    private readonly IRepository<Property> _roomRepository;
    private readonly IRepository<Booking> _bookingRepository;

    public RoomService(IRepository<Property> roomRepository, IRepository<Booking> bookingRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Property>> GetAvailableRoomsAsync(DateTimeOffset? checkIn, DateTimeOffset? checkOut)
    {
        var bookings = await _bookingRepository.GetAllAsync();
        var unavailableRoomIds = bookings
            .Where(b => b.CheckInDate < checkOut && b.CheckOutDate > checkIn) // Check if the booking overlaps with the requested dates
            .Select(b => b.PropertyId)
            .ToHashSet();

        var rooms = await _roomRepository.GetAllAsync();
        return rooms
            .Where(r => !unavailableRoomIds.Contains(r.Id));
    }
}
