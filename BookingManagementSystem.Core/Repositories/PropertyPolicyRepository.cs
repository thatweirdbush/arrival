using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class PropertyPolicyRepository : Repository<PropertyPolicy>
{
    public PropertyPolicyRepository(DbContext context) : base(context)
    {
    }
}
