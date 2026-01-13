using Feedback.Domain.Entities;
using Feedback.Domain.Interfaces;
using Feedback.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Infrastructure.Repositories;

/// <summary>
/// Feedback-specific repository implementation with custom queries
/// </summary>
public class FeedbackRepository : Repository<FeedbackEntity>, IFeedbackRepository
{
    public FeedbackRepository(FeedbackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FeedbackEntity>> GetByStatusAsync(FeedbackStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(f => !f.IsDeleted && f.Status == status)
            .OrderByDescending(f => f.SubmittedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<FeedbackEntity>> GetByRatingAsync(int rating, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(f => !f.IsDeleted && f.Rating == rating)
            .OrderByDescending(f => f.SubmittedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<FeedbackEntity>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(f => !f.IsDeleted && f.Category == category)
            .OrderByDescending(f => f.SubmittedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<FeedbackEntity>> GetRecentFeedbackAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(f => !f.IsDeleted)
            .OrderByDescending(f => f.SubmittedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
