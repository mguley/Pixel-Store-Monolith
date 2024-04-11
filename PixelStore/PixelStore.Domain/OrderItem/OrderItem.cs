using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Products;
using OrderInstance = PixelStore.Domain.Order.Order;

namespace PixelStore.Domain.OrderItem;

/// <summary>
/// Represents an item in an order, linking to a specific product.
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItem"/> class.
    /// </summary>
    public OrderItem()
    {
    }
    
    /// <summary>
    /// Gets or sets the order identifier this item is associated with.
    /// </summary>
    public int OrderId { get; private set; }
    
    /// <summary>
    /// Gets or sets the product identifier this item is referring to.
    /// </summary>
    public int ProductId { get; private set; }

    /// <summary>
    /// Gets or sets the quantity of the product ordered.
    /// </summary>
    public Quantity Quantity { get; private set; } = new Quantity(Value: default);

    /// <summary>
    /// Gets or sets the price per unit of the product at the time of order.
    /// </summary>
    public UnitPrice UnitPrice { get; private set; } = new UnitPrice(Value: default);
    
    /// <summary>
    /// Navigation property for the order.
    /// </summary>
    public virtual OrderInstance Order { get; private set; } = new OrderInstance();

    /// <summary>
    /// Navigation property for the product.
    /// </summary>
    public virtual Product Product { get; private set; } = new Product();
}
