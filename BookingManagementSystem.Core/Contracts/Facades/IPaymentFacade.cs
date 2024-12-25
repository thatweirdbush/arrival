using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Facades;
#nullable enable
public interface IPaymentFacade
{
    Task<Property?> GetPropertyByIdAsync(int id);
    Task<IEnumerable<Voucher>> GetAllVouchersAsync();
    Task UpdateVoucherAsync(Voucher voucher);
    Task AddBookingAsync(Booking booking);
    Task AddNotificationAsync(Notification notification);
}
