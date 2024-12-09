using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BookingManagementSystem.Core.Contracts.Repositories;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetSortedAsync<TKey>(Expression<Func<T, TKey>> keySelector, bool sortDescending);
    Task<IEnumerable<T>> GetFilteredAndSortedAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector, bool sortDescending);
    Task<IEnumerable<T>> GetSearchedAsync(Expression<Func<T, bool>> search);
    Task<int> CountAsync(Expression<Func<T, bool>> filter);
}
