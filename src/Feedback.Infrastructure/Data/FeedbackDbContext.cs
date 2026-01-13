using Feedback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Infrastructure.Data;

/// <summary>
/// Database context for the Feedback application
/// </summary>
public class FeedbackDbContext : DbContext
{
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
    {
    }

    public DbSet<FeedbackEntity> Feedbacks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure FeedbackEntity
        modelBuilder.Entity<FeedbackEntity>(entity =>
        {
            entity.ToTable("Feedbacks");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Email)
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(e => e.Comments)
                .HasMaxLength(1000);

            entity.Property(e => e.Category)
                .HasMaxLength(50);

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.Rating)
                .IsRequired();

            entity.Property(e => e.SubmittedAt)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            // Index for common queries
            entity.HasIndex(e => e.Rating);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.SubmittedAt);
        });
    }
}
