namespace PixelStore.Domain.Abstractions;

/// <summary>
/// Defines a generic repository interface for accessing entities of a specific type.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this repository works with.</typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Asynchronously retrieves all entities of type TEntity.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads
    /// to receive notice of cancellation.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an IEnumerable of TEntity.
    /// </returns>
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves an entity by its GUID identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads
    /// to receive notice of cancellation.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the entity found, or null if no entity was found.
    /// </returns>
    Task<TEntity?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads
    /// to receive notice of cancellation.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads
    /// to receive notice of cancellation.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is true if the entity was successfully updated; otherwise, false.
    /// </returns>
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes an entity from the repository by its GUID identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the entity to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads
    /// to receive notice of cancellation.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is true if the entity was successfully deleted; otherwise, false.
    /// </returns>
    Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default);
}
