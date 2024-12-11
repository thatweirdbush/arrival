using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BookingManagementSystem.Core.Contracts.Repositories;
public interface IRepository<T>
{
#nullable enable
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetSortedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool sortDescending);
    Task<IEnumerable<T>> GetPagedSortedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool sortDescending, int pageNumber, int pageSize);
    Task<IEnumerable<T>> GetFilteredAndSortedAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector, bool sortDescending);
    Task<IEnumerable<T>> GetPagedFilteredAndSortedAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector, bool sortDescending, int pageNumber, int pageSize);
    Task<IEnumerable<TResult>> GetMappedAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
    Task<IEnumerable<T>> GetSearchedAsync(Expression<Func<T, bool>> search);
    Task<int> CountAsync(Expression<Func<T, bool>> filter);
}
