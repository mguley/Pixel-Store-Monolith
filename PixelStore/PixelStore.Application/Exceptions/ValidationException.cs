namespace PixelStore.Application.Exceptions;

/// <summary>
/// Represents errors that occur during application validation processes.
/// It is thrown when validation fails for one or more fields in an operation that uses data annotations
/// or other validation mechanisms.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Gets the collection of <see cref="ValidationError"/> associated with this exception.
    /// These errors detail all the validation issues encountered during the operation.
    /// </summary>
    public IEnumerable<ValidationError> Errors { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specific sequence of
    /// <see cref="ValidationError"/> items detailing each validation error.
    /// </summary>
    /// <param name="errors">A collection of <see cref="ValidationError"/> objects,
    /// each representing a specific error in validation.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ValidationException(IEnumerable<ValidationError> errors) : 
        base(message: "One or more validation failures have occurred.")
    {
        Errors = errors ??
                 throw new ArgumentNullException(paramName: nameof(errors),
                     message: "Validation errors cannot be null.");
    }
}
