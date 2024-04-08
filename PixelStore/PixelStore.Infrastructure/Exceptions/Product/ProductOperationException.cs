namespace PixelStore.Infrastructure.Exceptions.Product;

/// <summary>
/// Represents errors that occur during product operation execution within the application.
/// </summary>
public class ProductOperationException : Exception
{
    /// <summary>
    /// Gets the unique identifier for the product related to the exception.
    /// </summary>
    public Guid ProductId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductOperationException"/> class.
    /// </summary>
    public ProductOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductOperationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ProductOperationException(string message) : base(message: message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductOperationException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public ProductOperationException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductOperationException"/> class with a specified error message,
    /// a reference to the inner exception that is the cause of this exception, and the unique identifier of the user
    /// related to the operation that caused the exception.
    /// </summary>
    /// <param name="productId">The globally unique identifier of the product related to the exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public ProductOperationException(Guid productId, string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
        ProductId = productId;
    }
}
