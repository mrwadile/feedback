using Feedback.Domain.Entities;

namespace Feedback.Application.DTOs;

/// <summary>
/// DTO for returning feedback data
/// </summary>
public class FeedbackDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public string? Category { get; set; }
    public FeedbackStatus Status { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
