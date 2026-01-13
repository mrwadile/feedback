using Feedback.Domain.Common;

namespace Feedback.Domain.Entities;

/// <summary>
/// Represents a feedback submission from a user
/// </summary>
public class FeedbackEntity : BaseEntity
{
    public string CustomerName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int Rating { get; set; } // 1-5 stars
    public string? Comments { get; set; }
    public string? Category { get; set; } // e.g., Service, Product, Support
    public FeedbackStatus Status { get; set; }
    public DateTime SubmittedAt { get; set; }
}

public enum FeedbackStatus
{
    Pending = 0,
    UnderReview = 1,
    Resolved = 2,
    Closed = 3
}
