using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Abstractions;
using PixelStore.Infrastructure.Exceptions.Application;
using PixelStore.Infrastructure.Exceptions.Database;

namespace PixelStore.Infrastructure;

/// <summary>
/// Represents the database context for the application, serving as the main class that coordinates
/// Entity Framework functionality for a given data model. This context is also an implementation
/// of the <see cref="IUnitOfWork"/> pattern.
/// </summary>
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class without specific options.
    /// </summary>
    public ApplicationDbContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options: options)
    {
    }

    /// <summary>
    /// Configures the schema needed for the model when the model is being created.
    /// This method applies configurations from all <see cref="IEntityTypeConfiguration{TEntity}"/> instances found
    /// within the same assembly as the context itself.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder: modelBuilder);
    }

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete,
    /// allowing the operation to be cancelled.</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    /// <exception cref="DataConcurrencyException">Thrown when a database concurrency conflict is detected, indicating
    /// that the data has been modified externally since it was loaded into memory.</exception>
    /// <exception cref="ApplicationOperationException">Thrown for other database update errors,
    /// encapsulating issues such as violations of database constraints.</exception>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new DataConcurrencyException(message: "A database concurrency error occurred.",
                innerException: exception);
        }
        catch (DbUpdateException exception)
        {
            throw new ApplicationOperationException(message: "An error occurred while updating the database.",
                innerException: exception);
        }
    }
}
