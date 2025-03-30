using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class ReviewRepository : Repository<Review>
{
    public ReviewRepository(DbContext context) : base(context)
    {
    }
}
