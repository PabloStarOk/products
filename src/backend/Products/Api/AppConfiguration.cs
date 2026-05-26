using Products.Api.Endpoints;
using Scalar.AspNetCore;

namespace Products.Api;

/// <summary>
/// Provides application-level configuration methods for the API.
/// </summary>
public static class AppConfiguration
{
    /// <summary>
    /// Configures the application's middleware and endpoint mappings.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    public static void ConfigureApp(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.UseHttpsRedirection();
        app.UseExceptionHandler("/error");
        app.UseStatusCodePages();
        ProductEndpointsGroup.Map(app);
    }
}