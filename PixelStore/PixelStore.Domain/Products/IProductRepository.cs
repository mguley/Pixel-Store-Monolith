using PixelStore.Domain.Abstractions;

namespace PixelStore.Domain.Products;

/// <summary>
/// Represents the repository interface for product entity.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Asynchronously determines whether any product entities exist in the repository.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The task result contains 'true' if any entities exist; otherwise, 'false'.</returns>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
