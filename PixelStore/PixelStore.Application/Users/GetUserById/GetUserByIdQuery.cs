using PixelStore.Application.Abstractions.Messaging;
using PixelStore.Application.Users.Shared;

namespace PixelStore.Application.Users.GetUserById;

/// <summary>
/// Represents a query to retrieve a user by its id.
/// </summary>
public sealed record GetUserByIdQuery : IQuery<UserResponseDto>
{
    public GetUserByIdQuery(Guid guid) => Guid = guid;
    
    public Guid Guid { get; }
}
