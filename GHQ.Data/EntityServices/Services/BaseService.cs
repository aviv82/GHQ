using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GHQ.Data.EntityServices.Services;

public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
{
    private readonly IGHQContext _context;

    public BaseService(IGHQContext context)
    {
        _context = context;
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.GetSet<T>().ToListAsync(cancellationToken);
    }
    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.GetSet<T>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<List<T>> FindManyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.GetSet<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<T> InsertAsync(T item, CancellationToken cancellationToken)
    {
        item.CreatedDate = DateTime.Now;
        item.UpdatedDate = DateTime.Now;
        await _context.GetSet<T>().AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task UpdateAsync(T item, CancellationToken cancellationToken)
    {
        item.UpdatedDate = DateTime.Now;
        _context.GetSet<T>().Update(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T item, CancellationToken cancellationToken)
    {
        _context.GetSet<T>().Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
