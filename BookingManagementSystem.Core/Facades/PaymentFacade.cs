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
    private readonly IRepository<Payment> _paymentRepository;
    private readonly INotificationService _notificationService;
    public Property? Property { get; private set; }

    public PaymentFacade(
        IRepository<Property> propertyRepository, 
        IRepository<Voucher> voucherRepository,
        IRepository<Booking> bookingRepository,
        IRepository<Payment> paymentRepository,
        INotificationService notificationService)
    {
        _propertyRepository = propertyRepository;
        _voucherRepository = voucherRepository;
        _bookingRepository = bookingRepository;
        _paymentRepository = paymentRepository;
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

    public async Task<Voucher?> GetVoucherByCodeAsync(string code)
    {
        Expression<Func<Voucher, bool>> IsVoucherValid = v
            => DateTime.Now.ToUniversalTime() >= v.ValidFrom.ToUniversalTime()
            && (!v.ValidUntil.HasValue || DateTime.Now.ToUniversalTime() <= v.ValidUntil.Value.ToUniversalTime())
            && !v.IsUsed;

        var vouchers = await _voucherRepository.GetAllAsync(IsVoucherValid);
        return vouchers.FirstOrDefault(v => v.Code == code);
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

    public async Task AddPaymentAsync(Payment payment)
    {
        await _paymentRepository.AddAsync(payment);
        await _paymentRepository.SaveChangesAsync();
    }

    public Task AddNotificationAsync(Notification notification)
    {
        return _notificationService.AddNotificationAsync(notification);
    }
}
