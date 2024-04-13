using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.OrderItem;
using PixelStore.Infrastructure.Exceptions.OrderItem;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="OrderItem"/> entity.
/// </summary>
internal sealed class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItemRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public OrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Retrieves an order item by its unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the order item to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The order item if found; otherwise null.</returns>
    public async Task<OrderItem?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<OrderItem>()
            .FirstOrDefaultAsync(predicate: orderItem => orderItem.Guid == guid, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates an existing order item.
    /// </summary>
    /// <param name="entity">An order item to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if success.</returns>
    /// <exception cref="OrderItemOperationException">Thrown if the order item is not found.</exception>
    public async Task<bool> UpdateAsync(OrderItem entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        OrderItem? orderItem = await DbContext.Set<OrderItem>()
            .FindAsync(keyValues: new object[] { entity.Guid }, cancellationToken: cancellationToken);

        if (orderItem is null)
        {
            throw new OrderItemOperationException(message: $"Order item with ID {entity.Guid} not found.");
        }
        
        DbContext.Entry(entity: orderItem).CurrentValues.SetValues(obj: entity);
        return true;
    }

    /// <summary>
    /// Deletes an order item by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the order item to delete.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if the order item was found and removed; otherwise, throws a <see cref="OrderItemOperationException"/>.</returns>
    /// <exception cref="OrderItemOperationException">Thrown when the order item cannot be found.</exception>
    public async Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        OrderItem? orderItem = await DbContext.Set<OrderItem>()
            .FirstOrDefaultAsync(predicate: orderItem => orderItem.Guid == guid, cancellationToken: cancellationToken);

        if (orderItem is null)
        {
            throw new OrderItemOperationException(message: $"Order item with ID {guid} not found.");
        }

        DbContext.Set<OrderItem>().Remove(entity: orderItem);
        return true;
    }
}
