using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Users;

namespace PixelStore.Domain.Customer;

/// <summary>
/// Represents a customer in the system, which may be a registered user or a guest.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Customer"/> class.
    /// </summary>
    public Customer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Customer"/> class with specified user details.
    /// </summary>
    /// <param name="guid">The globally unique identifier for the customer.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="email">Email address of the customer.</param>
    public Customer(Guid guid, int? userId, Email email)
    {
        Guid = guid;
        UserId = userId;
        Email = email;
    }

    /// <summary>
    /// Gets the globally unique identifier for the customer.
    /// </summary>
    public Guid Guid { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the identifier of the user associated with this customer, if any.
    /// </summary>
    public int? UserId { get; private set; }

    /// <summary>
    /// Gets or sets the email address of the customer, which is used to contact the guest customer.
    /// </summary>
    public Email Email { get; private set; } = new Email(Value: string.Empty);

    /// <summary>
    /// Navigation property for the associated user.
    /// </summary>
    public virtual User User { get; private set; } = new User();
}
