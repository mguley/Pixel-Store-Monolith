using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Products;
using PixelStore.Infrastructure.Exceptions.Product;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Product"/> entities.
/// </summary>
internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the product to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The product if found; otherwise, <c>null</c>.</returns>
    public async Task<Product?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Product>()
            .FirstOrDefaultAsync(predicate: product => product.Guid == guid, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Product? product = await DbContext.Set<Product>()
            .FindAsync(keyValues: new object[] { entity.Guid }, cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new ProductOperationException(message: $"Product with ID { entity.Guid } not found.");
        }
        
        DbContext.Entry(entity: product).CurrentValues.SetValues(obj: entity);
        return true;
    }

    /// <summary>
    /// Deletes a product by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the product to delete.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns><c>true</c> if the user was found and deleted; otherwise, throws a <see cref="ProductOperationException"/>.</returns>
    /// <exception cref="ProductOperationException">Thrown when the product cannot be found.</exception>
    public async Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        Product? product = await DbContext.Set<Product>()
            .FirstOrDefaultAsync(predicate: product => product.Guid == guid, cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new ProductOperationException(message: $"Product with ID { guid } not found.");
        }

        DbContext.Set<Product>().Remove(entity: product);
        return true;
    }
}
