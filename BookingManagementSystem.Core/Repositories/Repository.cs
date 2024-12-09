using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // Basic CRUD operations
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        // Check duplicate
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // Pagination
    public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    // Filtering
    public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AsQueryable().Where(filter).ToListAsync();
    }

    // Sorting, default ascending
    public async Task<IEnumerable<T>> GetSortedAsync<TKey>(
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending = false)
    {
        var sortedData = sortDescending
        ? _dbSet.OrderByDescending(keySelector)
        : _dbSet.OrderBy(keySelector);

        return await sortedData.ToListAsync();
    }

    // Filtering & Sorting
    public async Task<IEnumerable<T>> GetFilteredAndSortedAsync<TKey>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending = false)
    {
        var query = _dbSet.AsQueryable().Where(filter);
        return await (sortDescending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector)).ToListAsync();
    }

    // Searching by name, using Fuzzy Search
    // TODO: Implement Fuzzy Search
    public async Task<IEnumerable<T>> GetSearchedAsync(Expression<Func<T, bool>> search)
    {
        return await _dbSet.AsQueryable().Where(search).ToListAsync();
    }

    // Count total results when searching
    public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AsQueryable().CountAsync(filter);
    }
}