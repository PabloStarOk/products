using Products.Api.Endpoints;

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
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        ProductEndpointsGroup.Map(app);
    }
}