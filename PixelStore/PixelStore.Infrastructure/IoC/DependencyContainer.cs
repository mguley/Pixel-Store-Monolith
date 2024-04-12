using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Products;
using PixelStore.Domain.Users;
using PixelStore.Infrastructure.Exceptions.Application;
using PixelStore.Infrastructure.Repositories;

namespace PixelStore.Infrastructure.IoC;

/// <summary>
/// Provides an extension method for <see cref="IServiceCollection"/> to add and configure the infrastructure
/// layer's dependencies.
/// </summary>
public static class DependencyContainer
{
    /// <summary>
    /// Adds and configures the infrastructure-related services including the appropriate database context
    /// based on the application settings.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The application's configuration, used to access settings such as connection strings and provider selection.</param>
    /// <returns>The original <see cref="IServiceCollection"/> instance with the infrastructure services added,
    /// allowing for chaining of multiple calls.</returns>
    /// <exception cref="ApplicationOperationException">Thrown when the database configuration is missing or incomplete.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the specified database provider is unsupported.</exception>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        DatabaseSettings? dbConfig = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        if (dbConfig is null || string.IsNullOrWhiteSpace(dbConfig.DefaultConnection))
        {
            throw new ApplicationOperationException(message: "Database configuration must be specified.");
        }

        switch (dbConfig.DefaultConnection)
        {
            case "PostgreSQL":
                RegisterDatabaseContextPostgreSql(services: services, configuration: configuration);
                break;
            case "Sqlite":
                RegisterDatabaseContextSqlite(services: services, configuration: configuration);
                break;
            default:
                throw new InvalidOperationException(
                    message: $"Unsupported database provider: {dbConfig.DefaultConnection}");
        }

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    /// <summary>
    /// Registers the PostgreSQL database context.
    /// </summary>
    /// <param name="services">The service collection to add the database context to.</param>
    /// <param name="configuration">The application's configuration to retrieve the PostgreSQL connection string.</param>
    /// <exception cref="ArgumentNullException">Thrown if the PostgreSQL connection string is not found.</exception>
    private static void RegisterDatabaseContextPostgreSql(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(name: "DatabasePostgreSQL") ??
                                  throw new ArgumentNullException(paramName: nameof(configuration),
                                      message: "Invalid PostgreSQL DB configuration");
        services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
        {
            options.UseNpgsql(connectionString: connectionString);
        });
    }

    /// <summary>
    /// Registers the SQLite database context.
    /// </summary>
    /// <param name="services">The service collection to add the database context to.</param>
    /// <param name="configuration">The application's configuration to retrieve the SQLite connection string.</param>
    /// <exception cref="ArgumentNullException">Thrown if the SQLite connection string is not found.</exception>
    private static void RegisterDatabaseContextSqlite(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(name: "DatabaseSqlite") ??
                                  throw new ArgumentNullException(paramName: nameof(configuration),
                                      message: "Invalid DB configuration");
        services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
        {
            options.UseSqlite(connectionString: connectionString);
        });
    }
}
