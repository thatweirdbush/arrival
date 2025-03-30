using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class FAQRepository : Repository<FAQ>
{
    public FAQRepository(DbContext context) : base(context)
    {
    }
}
