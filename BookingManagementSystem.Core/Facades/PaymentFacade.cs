using System.Linq.Expressions;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Facades;
#nullable enable
public class PaymentFacade : IPaymentFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Voucher> _voucherRepository;
    private readonly IRepository<Booking> _bookingRepository;
    private readonly INotificationService _notificationService;
    public Property? Property { get; private set; }

    public PaymentFacade(
        IRepository<Property> propertyRepository, 
        IRepository<Voucher> voucherRepository,
        IRepository<Booking> bookingRepository,
        INotificationService notificationService)
    {
        _propertyRepository = propertyRepository;
        _voucherRepository = voucherRepository;
        _bookingRepository = bookingRepository;
        _notificationService = notificationService;
    }

    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        var query = _propertyRepository.GetQueryable();

        // Inlcude neccessary navigational properties
        query = query.Include(p => p.Reviews);

        Property = await query.FirstOrDefaultAsync(p => p.Id == id);
        return Property;
    }

    public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
    {
        Expression<Func<Voucher, bool>> IsVoucherValid = v
            => DateTime.Now.ToUniversalTime() >= v.ValidFrom.ToUniversalTime()
            && (!v.ValidUntil.HasValue || DateTime.Now.ToUniversalTime() <= v.ValidUntil.Value.ToUniversalTime())
            && !v.IsUsed;
        return await _voucherRepository.GetAllAsync(IsVoucherValid);
    }

    public async Task UpdateVoucherAsync(Voucher voucher)
    {
        await _voucherRepository.UpdateAsync(voucher);
        await _voucherRepository.SaveChangesAsync();
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _bookingRepository.AddAsync(booking);
        await _bookingRepository.SaveChangesAsync();
    }

    public Task AddNotificationAsync(Notification notification)
    {
        return _notificationService.AddNotificationAsync(notification);
    }
}
