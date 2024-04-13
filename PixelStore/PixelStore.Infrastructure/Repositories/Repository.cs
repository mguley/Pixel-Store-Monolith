using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Abstractions;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Provides a base implementation for a repository managing entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
internal abstract class Repository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext DbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    protected Repository(ApplicationDbContext dbContext) =>
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A collection of all entities.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Adds a new entity of type <typeparamref name="TEntity"/> to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await DbContext.AddAsync(entity: entity, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Asynchronously determines whether any entities of type <typeparamref name="TEntity"/> exist in the repository.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The task result contains 'true' if any entities exist; otherwise, 'false'.</returns>
    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().AnyAsync(cancellationToken: cancellationToken);
    }
}
