using System.Linq.Expressions;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class BookingRepository : Repository<Booking>
{
    public BookingRepository(DbContext context) : base(context)
    {
    }

    public async override Task<IEnumerable<Booking>> GetFilteredAndSortedAsync<TKey>(
    Expression<Func<Booking, bool>> filter,
    Expression<Func<Booking, TKey>> keySelector,
    bool sortDescending = false)
    {
        var query = _dbSet.AsQueryable()
            .Include(b => b.Property)
            .ThenInclude(p => p.Country)
            .Where(filter);
        return await (sortDescending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector)).ToListAsync();
    }
}
