namespace PixelStore.Domain.Abstractions;

/// <summary>
/// Defines a standard interface for domain result objects, encapsulating the outcome of a domain operation
/// with support for success status and error messaging.
/// </summary>
public interface IDomainResult
{
    /// <summary>
    /// Determines whether the operation was successful.
    /// </summary>
    bool IsSuccess { get; }
    
    /// <summary>
    /// Retrieves the error message if the operation failed.
    /// </summary>
    string GetErrorMessage { get; }
}
