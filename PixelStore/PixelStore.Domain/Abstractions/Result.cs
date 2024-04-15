namespace PixelStore.Domain.Abstractions;

/// <summary>
/// Represents a generic result of an operation, encapsulating the outcome's success or failure.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the operation if successful.</typeparam>
public sealed class Result<TValue>
{
    /// <summary>
    /// Gets the value produced by the operation if successful.
    /// </summary>
    public TValue? Value { get; }
    
    /// <summary>
    /// Holds the error message if the operation fails.
    /// </summary>
    private string Error { get; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess => string.IsNullOrEmpty(Error);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class for a successful outcome.
    /// </summary>
    /// <param name="value">The value resulting from a successful operation.</param>
    public Result(TValue value) => Value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class for a failed outcome.
    /// </summary>
    /// <param name="errorMessage">The error message describing the failure.</param>
    public Result(string errorMessage) => Error = errorMessage;
}
