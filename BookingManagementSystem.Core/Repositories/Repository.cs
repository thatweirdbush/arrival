using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Repositories;

namespace BookingManagementSystem.Core.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly List<T> _entities = [];

    // Basic CRUD operations
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.FromResult(_entities);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = _entities.FirstOrDefault(e => GetEntityId(e) == id);
        return await Task.FromResult(entity);
    }

    public async Task AddAsync(T entity)
    {
        _entities.Insert(0, entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(T entity)
    {
        var existingEntity = _entities.FirstOrDefault(e => GetEntityId(e) == GetEntityId(entity));
        if (existingEntity != null)
        {
            _entities.Remove(existingEntity);
            _entities.Add(entity);
        }
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = _entities.FirstOrDefault(e => GetEntityId(e) == id);
        if (entity != null)
        {
            _entities.Remove(entity);
        }
        await Task.CompletedTask;
    }

    private static int GetEntityId(T entity)
    {
        var property = typeof(T).GetProperty("Id");
        return property != null ? (int)property.GetValue(entity) : 0;
    }

    // Pagination
    public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var pagedData = _entities.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return await Task.FromResult(pagedData);
    }

    // Filtering
    public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter)
    {
        var filteredData = _entities.AsQueryable().Where(filter);
        return await Task.FromResult(filteredData);
    }

    // Sorting, default ascending
    public async Task<IEnumerable<T>> GetSortedAsync<TKey>(
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending = false)
    {
        var sortedData = sortDescending
            ? _entities.AsQueryable().OrderByDescending(keySelector)
            : _entities.AsQueryable().OrderBy(keySelector);

        return await Task.FromResult(sortedData);
    }

    // Filtering & Sorting
    public async Task<IEnumerable<T>> GetFilteredAndSortedAsync<TKey>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending = false)
    {
        var filteredData = _entities.AsQueryable().Where(filter);
        var sortedData = sortDescending
            ? filteredData.OrderByDescending(keySelector)
            : filteredData.OrderBy(keySelector);

        return await Task.FromResult(sortedData);
    }

    // Searching by name, using Fuzzy Search
    // TODO: Implement Fuzzy Search
    public async Task<IEnumerable<T>> GetSearchedAsync(Expression<Func<T, bool>> search)
    {
        var searchedData = _entities.AsQueryable().Where(search);
        return await Task.FromResult(searchedData);
    }

    // Count total results when searching
    public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
    {
        var count = _entities.AsQueryable().Count(filter);
        return await Task.FromResult(count);
    }
}