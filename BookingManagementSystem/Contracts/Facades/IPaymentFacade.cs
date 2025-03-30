using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Facades;
public interface IPaymentFacade
{
    Task<Property?> GetPropertyByIdAsync(int id);
    Task<Voucher?> GetVoucherByCodeAsync(string code);
    Task UpdateVoucherAsync(Voucher voucher);
    Task AddBookingAsync(Booking booking);
    Task AddPaymentAsync(Payment payment);
    Task AddNotificationAsync(Notification notification);
}
