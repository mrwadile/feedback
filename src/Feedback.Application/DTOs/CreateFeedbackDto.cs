namespace Feedback.Application.DTOs;

/// <summary>
/// DTO for creating a new feedback
/// </summary>
public class CreateFeedbackDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int Rating { get; set; } // 1-5
    public string? Comments { get; set; }
    public string? Category { get; set; }
}
