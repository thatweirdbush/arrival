using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class PropertyRepository : Repository<Property>
{
    public PropertyRepository(DbContext context) : base(context)
    {
    }

    public async override Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Country)
            .ToListAsync();
    }
}
