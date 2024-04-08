namespace PixelStore.Infrastructure.Exceptions.User;

/// <summary>
/// Represents errors that occur during user operation execution within the application.
/// </summary>
public class UserOperationException : Exception
{
    /// <summary>
    /// Gets the unique identifier for the user related to the exception.
    /// </summary>
    public Guid UserId { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationException"/> class.
    /// </summary>
    public UserOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public UserOperationException(string message) : base(message: message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public UserOperationException(string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationException"/> class with a specified error message,
    /// a reference to the inner exception that is the cause of this exception, and the unique identifier of the user
    /// related to the operation that caused the exception. 
    /// </summary>
    /// <param name="userId">The globally unique identifier of the user related to the exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception,
    /// or a null reference if not inner exception is specified.</param>
    public UserOperationException(Guid userId, string message, Exception innerException) :
        base(message: message, innerException: innerException)
    {
        UserId = userId;
    }
}
