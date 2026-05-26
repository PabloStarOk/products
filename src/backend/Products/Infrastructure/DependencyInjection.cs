using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services, such as the database context, into the application's dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// The name of the connection string to retrieve from the configuration.
    /// </summary>
    private const string ConnectionStringName = "DefaultConnection";

      /// <summary>
      /// Registers infrastructure services, including the application's DbContext, into the dependency injection container.
      /// </summary>
      /// <param name="services">The service collection to add services to.</param>
      /// <param name="configuration">The application configuration used to retrieve the connection string.</param>
      /// <exception cref="InvalidOperationException">Thrown if the connection string is not found in the configuration.</exception>
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(ConnectionStringName);
        if (connectionString is null)
        {
            throw new InvalidOperationException($"Could not find connection string, ensure is specified as: {ConnectionStringName}");
        }

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
    }
}