using FluentValidation;
using Products.Api.Endpoints;

namespace Products.Api;

/// <summary>
/// Provides extension methods for registering API-related services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds API-related services to the dependency injection container, including OpenAPI and validators.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    public static void AddApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddProblemDetails();
        services.AddTransient<IValidator<ProductRequest>, ProductRequestValidator>();
    }
}