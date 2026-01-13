using Feedback.Application.Interfaces;
using Feedback.Application.Services;
using Feedback.Domain.Interfaces;
using Feedback.Infrastructure.Data;
using Feedback.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Infrastructure;

/// <summary>
/// Dependency injection configuration for Infrastructure layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<FeedbackDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(FeedbackDbContext).Assembly.FullName)));

        // Register repositories
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Register services
        services.AddScoped<IFeedbackService, FeedbackService>();

        return services;
    }
}
