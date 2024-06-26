namespace PixelStore.Infrastructure.Exceptions.Application;

/// <summary>
/// Represents application errors that occur within the system.
/// </summary>
public class ApplicationOperationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationOperationException"/> class.
    /// </summary>
    public ApplicationOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationOperationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ApplicationOperationException(string message) : base(message: message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationOperationException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.
    /// </param>
    public ApplicationOperationException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }
}
