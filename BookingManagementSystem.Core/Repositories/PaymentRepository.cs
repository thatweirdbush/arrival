using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class PaymentRepository : Repository<Payment>
{
    public PaymentRepository(DbContext context) : base(context)
    {
    }
}
