using Microsoft.EntityFrameworkCore;

namespace PixelStore.WebApi.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IHost"/> to perform common startup tasks.
/// </summary>
internal static class HostExtensions
{
    /// <summary>
    /// Applies any pending migrations for the context on startup, and creates the database if it doesn't already exist.
    /// This method is typically called from the entry point of the application to ensure the database is updated to the
    /// latest migration before the application starts serving requests.
    /// </summary>
    /// <param name="host">The program's host representation, providing access to application services,
    /// configuration, and more.</param>
    /// <typeparam name="TContext">The type of the database context derived from <see cref="DbContext"/>
    /// representing the database to migrate.</typeparam>
    /// <returns>A task that represents the asynchronous operation of migrating the database.
    /// The task result contains the host itself, allowing for call chaining.</returns>
    public static async Task<IHost> MigrateDatabaseAsync<TContext>(this IHost host) where TContext : DbContext
    {
        using IServiceScope scope = host.Services.CreateScope();
        await using DbContext dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        await dbContext.Database.MigrateAsync();
        return host;
    }
}
