using GHQ.Data.Entities;
using System.Linq.Expressions;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IBaseService<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    //IQueryable<T> GetListWithQuery(IQueryWithFiltering filteringQuery, IQueryWithSorting sortingQuery, IQueryWithPagination paginationQuery);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> FindManyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<T> InsertAsync(T item, CancellationToken cancellationToken);
    Task UpdateAsync(T item, CancellationToken cancellationToken);
    Task DeleteAsync(T item, CancellationToken cancellationToken);

    //Task<T?> FirstOrDefaultAsync();
    //Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
}
