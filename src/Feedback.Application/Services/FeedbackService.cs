using Feedback.Application.Common;
using Feedback.Application.DTOs;
using Feedback.Application.Interfaces;
using Feedback.Domain.Entities;
using Feedback.Domain.Interfaces;

namespace Feedback.Application.Services;

/// <summary>
/// Implementation of feedback service with business logic
/// </summary>
public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<Result<FeedbackDto>> CreateFeedbackAsync(CreateFeedbackDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Validation
            if (string.IsNullOrWhiteSpace(dto.CustomerName))
                return Result<FeedbackDto>.Failure("Customer name is required");

            if (dto.Rating < 1 || dto.Rating > 5)
                return Result<FeedbackDto>.Failure("Rating must be between 1 and 5");

            // Map DTO to Entity
            var entity = new FeedbackEntity
            {
                Id = Guid.NewGuid(),
                CustomerName = dto.CustomerName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Rating = dto.Rating,
                Comments = dto.Comments,
                Category = dto.Category,
                Status = FeedbackStatus.Pending,
                SubmittedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var createdEntity = await _feedbackRepository.AddAsync(entity, cancellationToken);

            // Map Entity to DTO
            var feedbackDto = MapToDto(createdEntity);

            return Result<FeedbackDto>.Success(feedbackDto);
        }
        catch (Exception ex)
        {
            return Result<FeedbackDto>.Failure($"Error creating feedback: {ex.Message}");
        }
    }

    public async Task<Result<FeedbackDto>> GetFeedbackByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _feedbackRepository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
                return Result<FeedbackDto>.Failure("Feedback not found");

            var dto = MapToDto(entity);
            return Result<FeedbackDto>.Success(dto);
        }
        catch (Exception ex)
        {
            return Result<FeedbackDto>.Failure($"Error retrieving feedback: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<FeedbackDto>>> GetAllFeedbackAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _feedbackRepository.GetAllAsync(cancellationToken);
            var dtos = entities.Select(MapToDto);
            return Result<IEnumerable<FeedbackDto>>.Success(dtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<FeedbackDto>>.Failure($"Error retrieving feedback: {ex.Message}");
        }
    }

    public async Task<Result<FeedbackDto>> UpdateFeedbackAsync(UpdateFeedbackDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _feedbackRepository.GetByIdAsync(dto.Id, cancellationToken);

            if (entity == null)
                return Result<FeedbackDto>.Failure("Feedback not found");

            // Update only provided fields
            if (dto.Comments != null)
                entity.Comments = dto.Comments;

            if (dto.Rating.HasValue)
            {
                if (dto.Rating.Value < 1 || dto.Rating.Value > 5)
                    return Result<FeedbackDto>.Failure("Rating must be between 1 and 5");
                entity.Rating = dto.Rating.Value;
            }

            if (dto.Category != null)
                entity.Category = dto.Category;

            entity.UpdatedAt = DateTime.UtcNow;

            await _feedbackRepository.UpdateAsync(entity, cancellationToken);

            var feedbackDto = MapToDto(entity);
            return Result<FeedbackDto>.Success(feedbackDto);
        }
        catch (Exception ex)
        {
            return Result<FeedbackDto>.Failure($"Error updating feedback: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteFeedbackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var exists = await _feedbackRepository.ExistsAsync(id, cancellationToken);

            if (!exists)
                return Result<bool>.Failure("Feedback not found");

            await _feedbackRepository.DeleteAsync(id, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error deleting feedback: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<FeedbackDto>>> GetFeedbackByRatingAsync(int rating, CancellationToken cancellationToken = default)
    {
        try
        {
            if (rating < 1 || rating > 5)
                return Result<IEnumerable<FeedbackDto>>.Failure("Rating must be between 1 and 5");

            var entities = await _feedbackRepository.GetByRatingAsync(rating, cancellationToken);
            var dtos = entities.Select(MapToDto);
            return Result<IEnumerable<FeedbackDto>>.Success(dtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<FeedbackDto>>.Failure($"Error retrieving feedback: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<FeedbackDto>>> GetRecentFeedbackAsync(int count, CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _feedbackRepository.GetRecentFeedbackAsync(count, cancellationToken);
            var dtos = entities.Select(MapToDto);
            return Result<IEnumerable<FeedbackDto>>.Success(dtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<FeedbackDto>>.Failure($"Error retrieving feedback: {ex.Message}");
        }
    }

    private static FeedbackDto MapToDto(FeedbackEntity entity)
    {
        return new FeedbackDto
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Rating = entity.Rating,
            Comments = entity.Comments,
            Category = entity.Category,
            Status = entity.Status,
            SubmittedAt = entity.SubmittedAt,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
