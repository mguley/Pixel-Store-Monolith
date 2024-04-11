using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Users;

namespace PixelStore.Domain.Address;

/// <summary>
/// Represents a physical address in the system, associated optionally with a user.
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    public Address()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class with specified details.
    /// </summary>
    /// <param name="guid">The globally unique identifier for the address.</param>
    /// <param name="street">The street part of the address.</param>
    /// <param name="city">The city part of the address.</param>
    /// <param name="state">The state part of the address.</param>
    /// <param name="country">The country part of the address.</param>
    /// <param name="postalCode">The postal code part of the address.</param>
    /// <param name="userId">The optional user identifier if the address is linked to a registered user.</param>
    public Address(
        Guid guid,
        Street street,
        City city,
        State state,
        Country country,
        PostalCode postalCode,
        int? userId)
    {
        Guid = guid;
        Street = street;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        UserId = userId;
    }

    /// <summary>
    /// Gets the globally unique identifier for the address.
    /// </summary>
    public Guid Guid { get; private set; } = Guid.NewGuid();
    
    /// <summary>
    /// Gets the street part of the address.
    /// </summary>
    public Street Street { get; private set; } = new Street(Value: string.Empty);
    
    /// <summary>
    /// Gets the city part of the address.
    /// </summary>
    public City City { get; private set; } = new City(Value: string.Empty);
    
    /// <summary>
    /// Gets the state part of the address.
    /// </summary>
    public State State { get; private set; } = new State(Value: string.Empty);
    
    /// <summary>
    /// Gets the country part of the address.
    /// </summary>
    public Country Country { get; private set; } = new Country(Value: string.Empty);
    
    /// <summary>
    /// Gets the postal code part of the address.
    /// </summary>
    public PostalCode PostalCode { get; private set; } = new PostalCode(Value: string.Empty);

    /// <summary>
    /// Gets the optional user identifier if the address is linked to a registered user.
    /// </summary>
    public int? UserId { get; private set; }

    /// <summary>
    /// Navigation property for the associated user.
    /// </summary>
    public virtual User User { get; private set; } = new User();
}
