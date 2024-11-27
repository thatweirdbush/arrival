using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Facades;
public class PaymentFacade : IPaymentFacade
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<Voucher> _voucherRepository;
    private readonly IRepository<Booking> _bookingRepository; 

    public PaymentFacade(
        IRepository<Property> propertyRepository, 
        IRepository<Voucher> voucherRepository,
        IRepository<Booking> bookingRepository)
    {
        _propertyRepository = propertyRepository;
        _voucherRepository = voucherRepository;
        _bookingRepository = bookingRepository;

    }
    public async Task<Property?> GetPropertyByIdAsync(int id) =>
        await _propertyRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Voucher>> GetVouchersAsync() =>
        await _voucherRepository.GetAllAsync();
    public async Task AddBookingAsync(Booking booking) =>
        await _bookingRepository.AddAsync(booking);
}
