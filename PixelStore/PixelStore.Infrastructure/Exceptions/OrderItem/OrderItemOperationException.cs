namespace PixelStore.Infrastructure.Exceptions.OrderItem;

/// <summary>
/// Represents order item errors that occur within the system.
/// </summary>
public class OrderItemOperationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItemOperationException"/> class.
    /// </summary>
    public OrderItemOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItemOperationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public OrderItemOperationException(string message) : base(message: message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItemOperationException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public OrderItemOperationException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }
}
