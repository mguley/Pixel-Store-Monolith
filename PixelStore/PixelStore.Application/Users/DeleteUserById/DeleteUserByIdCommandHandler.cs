using MediatR;
using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Users;
using Exception = System.Exception;

namespace PixelStore.Application.Users.DeleteUserById;

/// <summary>
/// Handles the removal of a user by its ID from the system.
/// </summary>
internal sealed class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, Result<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserByIdCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository to interact with the data later.</param>
    /// <param name="unitOfWork">The unit of work that coordinates the work of multiple repositories by creating
    /// a single database context class shared among all of them.</param>
    public DeleteUserByIdCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(userRepository);
        ArgumentNullException.ThrowIfNull(unitOfWork);
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Handles the removal of a user by their ID.
    /// </summary>
    /// <param name="request">The command containing the user's GUID to be deleted.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of the command.</returns>
    public async Task<Result<bool>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User? userEntity =
                await _userRepository.GetByIdAsync(guid: request.UserId, cancellationToken: cancellationToken);

            if (userEntity is null)
            {
                return new Result<bool>(errorMessage: $"User with ID { request.UserId } not found.");
            }
            
            await _userRepository.DeleteByIdAsync(guid: userEntity.Guid, cancellationToken: cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
            return new Result<bool>(value: true);
        }
        catch (Exception exception)
        {
            return new Result<bool>(errorMessage: exception.Message);
        }
    }
}
