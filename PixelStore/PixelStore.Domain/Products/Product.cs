using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Products.Price;

namespace PixelStore.Domain.Products;

/// <summary>
/// Represents a product in the system, including its name, price, and description.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    /// <param name="guid">The globally unique identifier for the product.</param>
    /// <param name="name">The name of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="description">The description of the product.</param>
    public Product(Guid guid, Name name, Money price, Description description)
    {
        Guid = guid;
        Name = name;
        Price = price;
        Description = description;
    }
    
    /// <summary>
    /// Gets the globally unique identifier for the product.
    /// </summary>
    public Guid Guid { get; private set; }
    
    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    public Name Name { get; private set; }
    
    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public Money Price { get; private set; }
    
    /// <summary>
    /// Gets the description of the product.
    /// </summary>
    public Description Description { get; private set; }
}
