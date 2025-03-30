using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class AmenityRepository : Repository<Amenity>
{
    public AmenityRepository(DbContext context) : base(context)
    {
    }
}
