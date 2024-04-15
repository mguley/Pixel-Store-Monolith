using MediatR;
using PixelStore.Application.Users.Shared;
using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Users;
using Exception = System.Exception;

namespace PixelStore.Application.Users.GetUsersList;

/// <summary>
/// Handles the retrieval of all users from the system.
/// </summary>
internal sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserResponseDto>>>
{
    private readonly IUserRepository _userRepository;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllUsersQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository to interact with the data layer.</param>
    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(userRepository);
        _userRepository = userRepository;
    }
    
    /// <summary>
    /// Handles the <see cref="GetAllUsersQuery"/> and retrieves all users.
    /// </summary>
    /// <param name="request">The query to handle which is an instance of <see cref="GetAllUsersQuery"/>.</param>
    /// <param name="cancellationToken">Notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of the query.</returns>
    public async Task<Result<List<UserResponseDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync(cancellationToken: cancellationToken);
            List<UserResponseDto> userDtos = users.Select(user => new UserResponseDto
                {
                    Id = user.Guid,
                    Email = user.Email.Value,
                    FirstName = user.FirstName.Value,
                    LastName = user.LastName.Value
                })
                .ToList();

            return new Result<List<UserResponseDto>>(value: userDtos);
        }
        catch (Exception exception)
        {
            return new Result<List<UserResponseDto>>(errorMessage: exception.Message);
        }
    }
}
