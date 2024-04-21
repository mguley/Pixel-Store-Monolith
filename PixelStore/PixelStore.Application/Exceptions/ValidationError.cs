namespace PixelStore.Application.Exceptions;

/// <summary>
/// Represents a validation error with the property name and error message.
/// This record is used to encapsulate details about validation failures typically during model
/// validation within an application.
/// </summary>
public sealed record ValidationError
{
    /// <summary>
    /// Gets the name of the property that caused the validation failure.
    /// </summary>
    public string PropertyName { get; init; }
    
    /// <summary>
    /// Gets the message that describes the validation failure.
    /// </summary>
    public string ErrorMessage { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> record class.
    /// </summary>
    /// <param name="name">The name of the property that caused the validation failure.</param>
    /// <param name="message">The message that describes the validation failure.</param>
    public ValidationError(string name, string message)
    {
        PropertyName = name;
        ErrorMessage = message;
    }
}
