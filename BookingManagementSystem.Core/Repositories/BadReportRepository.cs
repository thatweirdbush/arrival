using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class BadReportRepository : Repository<BadReport>
{
    public BadReportRepository(DbContext context) : base(context)
    {
    }
}
