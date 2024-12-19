using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Facades;
#nullable enable
public class PaymentFacade : IPaymentFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Voucher> _voucherRepository;
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Notification> _notificationRepository;
    public Property? Property { get; private set; }

    public PaymentFacade(
        IRepository<Property> propertyRepository, 
        IRepository<Voucher> voucherRepository,
        IRepository<Booking> bookingRepository,
        IRepository<Notification> notificationRepository)
    {
        _propertyRepository = propertyRepository;
        _voucherRepository = voucherRepository;
        _bookingRepository = bookingRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        var query = _propertyRepository.GetQueryable();

        // Inlcude neccessary navigational properties
        query = query.Include(p => p.Reviews);

        Property = await query.FirstOrDefaultAsync(p => p.Id == id);
        return Property;
    }

    public async Task<IEnumerable<Voucher>> GetVouchersAsync() => await _voucherRepository.GetAllAsync();

    public async Task AddBookingAsync(Booking booking) => await _bookingRepository.AddAsync(booking);

    public async Task AddNotificationAsync(Notification notification) => await _notificationRepository.AddAsync(notification);
}
