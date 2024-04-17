using MediatR;
using PixelStore.Application.Users.Shared;
using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Users;
using Exception = System.Exception;

namespace PixelStore.Application.Users.GetUserById;

/// <summary>
/// Handles the retrieval of a user by its ID from the system.
/// </summary>
internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserResponseDto>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository to interact with the data layer.</param>
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(userRepository);
        _userRepository = userRepository;
    }
    
    /// <summary>
    /// Handles the retrieval of a user by their ID.
    /// </summary>
    /// <param name="request">The query request to retrieve a user, containing the user's ID.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of the query.</returns>
    /// <remarks>
    /// Method is responsible for fetching a user based on the ID provided in the <paramref name="request"/>
    /// It attempts to find the user, and if found, maps the user entity to a <see cref="UserResponseDto"/>.
    /// If the user is not found, it returns an error result.
    /// </remarks>
    public async Task<Result<UserResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            User? userEntity =
                await _userRepository.GetByIdAsync(guid: request.Guid, cancellationToken: cancellationToken);
            
            if (userEntity is null)
            {
                return new Result<UserResponseDto>(errorMessage: $"User with ID { request.Guid } not found.");
            }
            
            UserResponseDto userDto = new UserResponseDto
            {
                Id = userEntity.Guid,
                Email = userEntity.Email.Value,
                FirstName = userEntity.FirstName.Value,
                LastName = userEntity.LastName.Value
            };

            return new Result<UserResponseDto>(value: userDto);
        }
        catch (Exception exception)
        {
            return new Result<UserResponseDto>(errorMessage: exception.Message);
        }
    }
}
