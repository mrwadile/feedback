using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Feedback.Web.Models;
using Feedback.Application.Interfaces;
using Feedback.Application.DTOs;

namespace Feedback.Web.Controllers;

public class ServiceEvaluatorController : Controller
{
    private readonly ILogger<ServiceEvaluatorController> _logger;
    private readonly IFeedbackService _feedbackService;

    public ServiceEvaluatorController(ILogger<ServiceEvaluatorController> logger, IFeedbackService feedbackService)
    {
        _logger = logger;
        _feedbackService = feedbackService;
    }

    public IActionResult Survey()
    {
        return View();
    }

    public IActionResult Questions()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitFeedback([FromBody] CreateFeedbackDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        // Hardcode some defaults for this simple UI
        if (string.IsNullOrEmpty(dto.CustomerName)) dto.CustomerName = "Anonymous";
        if (string.IsNullOrEmpty(dto.Category)) dto.Category = "General";

        var result = await _feedbackService.CreateFeedbackAsync(dto);

        if (result.IsSuccess)
        {
            return Ok(new { success = true });
        }

        return BadRequest(new { success = false, message = result.ErrorMessage });
    }
}
