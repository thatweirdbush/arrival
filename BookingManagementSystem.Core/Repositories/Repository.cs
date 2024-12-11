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
#nullable enable
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    /// Basic CRUD operations
    /// <summary>
    /// Get all entities, support filtering
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async virtual Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        var query = _dbSet.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }

    /// <summary>
    /// Get an entity by its ID, return null if not found
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async virtual Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Add an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async virtual Task AddAsync(T entity)
    {
        // Check duplicate
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async virtual Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Delete an entity by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async virtual Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    /// <summary>
    /// Save changes to the database
    /// </summary>
    /// <returns></returns>
    public async virtual Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Support pagination
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async virtual Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    /// Support filtering
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async virtual Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AsQueryable().Where(filter).ToListAsync();
    }

    /// <summary>
    /// Support sorting, default ascending
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="keySelector"></param>
    /// <param name="sortDescending"></param>
    /// <returns></returns>
    public async virtual Task<IEnumerable<T>> GetSortedAsync<TKey>(
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending = false)
    {
        var sortedData = sortDescending
            ? _dbSet.OrderByDescending(keySelector)
            : _dbSet.OrderBy(keySelector);

        return await sortedData.ToListAsync();
    }

    /// <summary>
    /// Support filtering and sorting
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="filter"></param>
    /// <param name="keySelector"></param>
    /// <param name="sortDescending"></param>
    /// <returns></returns>
    public async virtual Task<IEnumerable<T>> GetFilteredAndSortedAsync<TKey>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending = false)
    {
        var query = _dbSet.AsQueryable().Where(filter);
        return await (sortDescending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector)).ToListAsync();
    }

    /// <summary>
    /// Support pagination and sorting, default ascending
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="keySelector"></param>
    /// <param name="sortDescending"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetPagedSortedAsync<TKey>(
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending,
        int pageNumber,
        int pageSize)
    {
        var query = _dbSet.AsQueryable();

        query = sortDescending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    /// Support pagination, filtering and sorting
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="filter"></param>
    /// <param name="keySelector"></param>
    /// <param name="sortDescending"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetPagedFilteredAndSortedAsync<TKey>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending,
        int pageNumber,
        int pageSize)
    {
        var query = _dbSet.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = sortDescending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    /// Support returning DTO or ViewModel data types
    /// Instead of returning entities directly, the repository can support returning DTO data types when needed.
    /// This is useful to offload unnecessary data when only certain fields are needed.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="filter"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TResult>> GetMappedAsync<TResult>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TResult>> selector)
    {
        return await _dbSet.AsQueryable()
            .Where(filter)
            .Select(selector)
            .ToListAsync();
    }

    /// <summary>
    /// Support Fuzzy Search
    /// TODO: Implement Fuzzy Search
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async virtual Task<IEnumerable<T>> GetSearchedAsync(Expression<Func<T, bool>> search)
    {
        return await _dbSet.AsQueryable().Where(search).ToListAsync();
    }

    /// <summary>
    /// Count the number of entities that satisfy the filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async virtual Task<int> CountAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AsQueryable().CountAsync(filter);
    }
}