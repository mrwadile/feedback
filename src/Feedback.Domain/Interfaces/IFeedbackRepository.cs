using Feedback.Domain.Entities;

namespace Feedback.Domain.Interfaces;

/// <summary>
/// Feedback-specific repository interface with custom queries
/// </summary>
public interface IFeedbackRepository : IRepository<FeedbackEntity>
{
    Task<IEnumerable<FeedbackEntity>> GetByStatusAsync(FeedbackStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<FeedbackEntity>> GetByRatingAsync(int rating, CancellationToken cancellationToken = default);
    Task<IEnumerable<FeedbackEntity>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
    Task<IEnumerable<FeedbackEntity>> GetRecentFeedbackAsync(int count, CancellationToken cancellationToken = default);
}
