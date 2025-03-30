using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class VoucherRepository : Repository<Voucher>
{
    public VoucherRepository(DbContext context) : base(context)
    {
    }
}
