using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class VoucherRepository : Repository<Voucher>
{
    public VoucherRepository(DbContext context) : base(context)
    {
    }
}
