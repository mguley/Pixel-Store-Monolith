namespace PixelStore.Application.Users.Shared;

/// <summary>
/// Represents a data transfer object for user information.
/// This DTO is used to communicate user details from application layers to client-facing parts of the application,
/// ensuring that sensitive information is handled properly.
/// </summary>
public sealed class UserResponseDto
{
    /// <summary>
    /// Gets the globally unique identifier for the user.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets the first name of the user.
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the last name of the user.
    /// </summary>
    public string LastName { get; init; } = string.Empty;
}
