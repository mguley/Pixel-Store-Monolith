using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelStore.Infrastructure.IoC;

/// <summary>
/// Provides an extension method for <see cref="IServiceCollection"/> to add and configure the infrastructure
/// layer's dependencies.
/// </summary>
public static class DependencyContainer
{
    /// <summary>
    /// Adds and configures the infrastructure-related services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The application's configuration, used to access settings such as connection strings.</param>
    /// <returns>The original <see cref="IServiceCollection"/> instance with the infrastructure services added,
    /// allowing for chaining of multiple calls.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterDatabaseContext(services: services, configuration: configuration);

        return services;
    }

    /// <summary>
    /// Configures and registers persistence-related services, such as the database context,
    /// with the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The application's configuration, used to get the database connection string.</param>
    /// <exception cref="ArgumentNullException">Thrown if the database connection string is not found
    /// in the application's configuration.</exception>
    private static void RegisterDatabaseContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(name: "Database") ??
                                  throw new ArgumentNullException(paramName: nameof(configuration),
                                      message: "Invalid DB configuration");
        services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
        {
            options.UseSqlite(connectionString: connectionString);
        });
    }
}
