using domain.Entities;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace infrastructure;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);
    }

    public  async Task InsertAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> Table()
    {
        return _context.Set<T>();
    }
}