using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Products.Price;

namespace PixelStore.Domain.Products;

/// <summary>
/// Represents a product in the system, including its name, price, and description.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Constructor used by EF Core for proxy generation and materialization.
    /// </summary>
    public Product()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="description">The description of the product.</param>
    public Product(Name name, Money price, Description description)
    {
        Name = name;
        Price = price;
        Description = description;
    }

    /// <summary>
    /// Gets the globally unique identifier for the product.
    /// This identifier is used to uniquely distinguish each product within the system.
    /// </summary>
    public Guid Guid { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the name of the product. The name is used for identifying the product in a more human-readable form.
    /// </summary>
    public Name Name { get; private set; } = new Name(Value: string.Empty);

    /// <summary>
    /// Gets the price of the product. The price represents the cost of the product, encapsulating both the amount and the currency.
    /// </summary>
    public Money Price { get; private set; } = new Money(Amount: default, Currency: Currency.None);

    /// <summary>
    /// Gets the description of the product. The description provides additional information about the product,
    /// which can include details about its features, benefits, and usage.
    /// </summary>
    public Description Description { get; private set; } = new Description(Value: string.Empty);
}
