using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Order;
using PixelStore.Infrastructure.Exceptions.Order;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Order"/> entity.
/// </summary>
internal sealed class OrderRepository : Repository<Order>, IOrderRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the order to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The order if found; otherwise null.</returns>
    public async Task<Order?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Order>()
            .FirstOrDefaultAsync(predicate: order => order.Guid == guid, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="entity">An order to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if success.</returns>
    /// <exception cref="OrderOperationException">Thrown if the order is not found.</exception>
    public async Task<bool> UpdateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Order? order = await DbContext.Set<Order>()
            .FindAsync(keyValues: new object[] { entity.Guid }, cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderOperationException(message: $"Order with ID { entity.Guid } not found.");
        }
        
        DbContext.Entry(entity: order).CurrentValues.SetValues(obj: entity);
        return true;
    }

    /// <summary>
    /// Deletes an order by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the order to delete.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if the order was found and removed; otherwise, throws a <see cref="OrderOperationException"/>.</returns>
    /// <exception cref="OrderOperationException">Thrown when the order cannot be found.</exception>
    public async Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        Order? order = await DbContext.Set<Order>()
            .FirstOrDefaultAsync(predicate: order => order.Guid == guid, cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderOperationException(message: $"Order with ID {guid} not found.");
        }

        DbContext.Set<Order>().Remove(entity: order);
        return true;
    }
}
