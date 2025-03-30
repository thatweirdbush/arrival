using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class UserRepository : Repository<User>
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}
