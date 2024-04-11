using PixelStore.Domain.Abstractions;
using Item = PixelStore.Domain.OrderItem.OrderItem;
using CustomerInstance = PixelStore.Domain.Customer.Customer;

namespace PixelStore.Domain.Order;

/// <summary>
/// Represents an order made by a customer.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class.
    /// </summary>
    public Order()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class with specified details.
    /// </summary>
    /// <param name="customerId">The customer identifier for the order.</param>
    /// <param name="orderDate">The date when the order was placed.</param>
    public Order(int customerId, DateTime orderDate)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
    }

    /// <summary>
    /// Gets or sets the identifier of the customer who placed the order.
    /// </summary>
    public int CustomerId { get; private set; }
    
    /// <summary>
    /// Gets or sets the date when the order was placed.
    /// </summary>
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Navigation property for the customer who placed the order.
    /// </summary>
    public virtual CustomerInstance Customer { get; private set; } = new CustomerInstance();
    
    /// <summary>
    /// Collection of order items that belong to this order.
    /// </summary>
    public virtual ICollection<Item> OrderItems { get; private set; } = new List<Item>();
}
