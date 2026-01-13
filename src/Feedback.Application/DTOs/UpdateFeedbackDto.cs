namespace Feedback.Application.DTOs;

/// <summary>
/// DTO for updating existing feedback
/// </summary>
public class UpdateFeedbackDto
{
    public Guid Id { get; set; }
    public string? Comments { get; set; }
    public int? Rating { get; set; }
    public string? Category { get; set; }
}
