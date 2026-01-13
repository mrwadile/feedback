using Feedback.Application.Common;
using Feedback.Application.DTOs;

namespace Feedback.Application.Interfaces;

/// <summary>
/// Service interface for feedback operations
/// </summary>
public interface IFeedbackService
{
    Task<Result<FeedbackDto>> CreateFeedbackAsync(CreateFeedbackDto dto, CancellationToken cancellationToken = default);
    Task<Result<FeedbackDto>> GetFeedbackByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<FeedbackDto>>> GetAllFeedbackAsync(CancellationToken cancellationToken = default);
    Task<Result<FeedbackDto>> UpdateFeedbackAsync(UpdateFeedbackDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteFeedbackAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<FeedbackDto>>> GetFeedbackByRatingAsync(int rating, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<FeedbackDto>>> GetRecentFeedbackAsync(int count, CancellationToken cancellationToken = default);
}
