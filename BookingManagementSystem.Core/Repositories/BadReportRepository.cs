using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class BadReportRepository : Repository<BadReport>
{
    public BadReportRepository(DbContext context) : base(context)
    {
    }
}
