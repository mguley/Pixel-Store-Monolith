using PixelStore.Application.Abstractions.Messaging.Query;
using PixelStore.Application.Users.Shared;

namespace PixelStore.Application.Users.GetUsersList;

/// <summary>
/// Represents a query to retrieve all users from the system.
/// </summary>
public sealed record GetAllUsersQuery : IQuery<List<UserResponseDto>>
{
}
