using PixelStore.Domain.Abstractions;
using AddressItem = PixelStore.Domain.Address.Address;

namespace PixelStore.Domain.Users;

/// <summary>
/// Represents a user entity in the system.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Constructor used by EF Core for proxy generation and materialization.
    /// </summary>
    public User()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the User class with specified details.
    /// </summary>
    /// <param name="guid">The globally unique identifier for the user.</param>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <param name="email">The user's email address.</param>
    public User(Guid guid, FirstName firstName, LastName lastName, Email email)
    {
        Guid = guid;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    /// <summary>
    /// Gets the globally unique identifier for the user.
    /// This identifier is used to uniquely distinguish each product within the system.
    /// </summary>
    public Guid Guid { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the user's first name.
    /// </summary>
    public FirstName FirstName { get; private set; } = new FirstName(Value: string.Empty);

    /// <summary>
    /// Gets the user's last name.
    /// </summary>
    public LastName LastName { get; private set; } = new LastName(Value: string.Empty);

    /// <summary>
    /// Gets the user's email address.
    /// </summary>
    public Email Email { get; private set; } = new Email(Value: string.Empty);

    /// <summary>
    /// List of assigned addresses per user.
    /// </summary>
    public virtual ICollection<AddressItem> Addresses { get; init; } = new List<AddressItem>();
}
