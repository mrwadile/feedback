using Feedback.Domain.Common;
using Feedback.Domain.Interfaces;
using Feedback.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for basic CRUD operations
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly FeedbackDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(FeedbackDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => !e.IsDeleted)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => !e.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            // Soft delete
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await UpdateAsync(entity, cancellationToken);
        }
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => !e.IsDeleted)
            .AnyAsync(e => e.Id == id, cancellationToken);
    }
}
