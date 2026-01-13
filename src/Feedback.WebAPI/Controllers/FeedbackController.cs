using Feedback.Application.DTOs;
using Feedback.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.WebAPI.Controllers;

/// <summary>
/// API Controller for managing feedback
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;
    private readonly ILogger<FeedbackController> _logger;

    public FeedbackController(IFeedbackService feedbackService, ILogger<FeedbackController> logger)
    {
        _feedbackService = feedbackService;
        _logger = logger;
    }

    /// <summary>
    /// Get all feedback
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FeedbackDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all feedback");
        var result = await _feedbackService.GetAllFeedbackAsync(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    /// <summary>
    /// Get feedback by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting feedback with ID: {Id}", id);
        var result = await _feedbackService.GetFeedbackByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);

        return Ok(result.Data);
    }

    /// <summary>
    /// Create new feedback
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateFeedbackDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new feedback for customer: {CustomerName}", dto.CustomerName);
        var result = await _feedbackService.CreateFeedbackAsync(dto, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    /// <summary>
    /// Update existing feedback
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFeedbackDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch");

        _logger.LogInformation("Updating feedback with ID: {Id}", id);
        var result = await _feedbackService.UpdateFeedbackAsync(dto, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);

        return Ok(result.Data);
    }

    /// <summary>
    /// Delete feedback
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting feedback with ID: {Id}", id);
        var result = await _feedbackService.DeleteFeedbackAsync(id, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);

        return NoContent();
    }

    /// <summary>
    /// Get feedback by rating
    /// </summary>
    [HttpGet("rating/{rating}")]
    [ProducesResponseType(typeof(IEnumerable<FeedbackDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByRating(int rating, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting feedback with rating: {Rating}", rating);
        var result = await _feedbackService.GetFeedbackByRatingAsync(rating, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    /// <summary>
    /// Get recent feedback
    /// </summary>
    [HttpGet("recent/{count}")]
    [ProducesResponseType(typeof(IEnumerable<FeedbackDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecent(int count = 10, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting {Count} recent feedback", count);
        var result = await _feedbackService.GetRecentFeedbackAsync(count, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }
}
