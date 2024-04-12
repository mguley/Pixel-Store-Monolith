namespace PixelStore.Infrastructure.Exceptions.Address;

/// <summary>
/// Represents errors that occur during address operation execution within the application.
/// </summary>
public class AddressOperationException : Exception
{
    /// <summary>
    /// Gets the unique identifier for the product related to the exception.
    /// </summary>
    public Guid AddressId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressOperationException"/> class.
    /// </summary>
    public AddressOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressOperationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public AddressOperationException(string message) : base(message: message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressOperationException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public AddressOperationException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressOperationException"/> class with a specified error message,
    /// a reference to the inner exception that is the cause of this exception, and the unique identifier of the user
    /// related to the operation that caused the exception.
    /// </summary>
    /// <param name="addressId">The globally unique identifier of the address related to the exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public AddressOperationException(Guid addressId, string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
        AddressId = addressId;
    }
}
