using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
#nullable enable
public class PropertyRepository : Repository<Property>
{
    public PropertyRepository(DbContext context) : base(context)
    {
    }

    public async override Task<IEnumerable<Property>> GetAllAsync(Expression<Func<Property, bool>>? filter = null)
    {
        var query = _dbSet.AsQueryable();
        query = query.Include(p => p.Country);

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }
}
