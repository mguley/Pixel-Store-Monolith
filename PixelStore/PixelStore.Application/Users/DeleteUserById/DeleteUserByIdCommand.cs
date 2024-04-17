using PixelStore.Application.Abstractions.Messaging.Command;

namespace PixelStore.Application.Users.DeleteUserById;

/// <summary>
/// Command to delete a user identified by their unique identifier.
/// </summary>
/// <param name="UserId">The unique identifier of the user to be deleted.</param>
public sealed record DeleteUserByIdCommand(Guid UserId) : ICommand<bool>;
