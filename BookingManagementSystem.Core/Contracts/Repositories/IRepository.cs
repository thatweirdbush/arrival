using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BookingManagementSystem.Core.Commons.Paginations;

namespace BookingManagementSystem.Core.Contracts.Repositories;
public interface IRepository<T>
{
#nullable enable
    IQueryable<T> GetQueryable();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(int id);
    Task DeleteRangeAsync(IEnumerable<int> ids);
    Task DeleteAllAsync();
    Task SaveChangesAsync();
    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetSortedAsync<TKey>(
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending);
    Task<IEnumerable<T>> GetPagedSortedAsync<TKey>(
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending,
        int pageNumber,
        int pageSize);
    Task<IEnumerable<T>> GetFilteredAndSortedAsync<TKey>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TKey>> keySelector,
        bool sortDescending);
    Task<PaginatedResult<T>> GetPagedFilteredAndSortedAsync(
        Func<IQueryable<T>, IQueryable<T>> queryBuilder,
        Expression<Func<T, object>> keySelector,
        bool sortDescending,
        int pageNumber,
        int pageSize);
    Task<IEnumerable<TResult>> GetMappedAsync<TResult>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T,TResult>> selector);
    Task<IEnumerable<T>> GetSearchedAsync(Expression<Func<T, bool>> search);
    Task<int> CountAsync(Expression<Func<T, bool>> filter);
}
