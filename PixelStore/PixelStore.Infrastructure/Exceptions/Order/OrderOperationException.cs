namespace PixelStore.Infrastructure.Exceptions.Order;

/// <summary>
/// Represents order errors that occur within the system.
/// </summary>
public class OrderOperationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderOperationException"/> class.
    /// </summary>
    public OrderOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderOperationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public OrderOperationException(string message) : base(message: message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderOperationException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public OrderOperationException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }
}
