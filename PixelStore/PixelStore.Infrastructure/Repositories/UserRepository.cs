using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Users;
using PixelStore.Infrastructure.Exceptions.User;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="User"/> entities.
/// </summary>
internal sealed class UserRepository : Repository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the user to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The user if found; otherwise, <c>null</c>.</returns>
    public async Task<User?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .FirstOrDefaultAsync(predicate: user => user.Guid == guid, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="entity">The user entity to update.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns><c>true</c> if the user exists and was updated; otherwise, throws a <see cref="UserOperationException"/>.</returns>
    /// <exception cref="UserOperationException">Thrown when the user cannot be found.</exception>
    public async Task<bool> UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        User? user = await DbContext.Set<User>()
            .FindAsync(keyValues: new object[] { entity.Guid }, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new UserOperationException(message: $"User with ID { entity.Guid } not found.");
        }
        
        DbContext.Entry(entity: user).CurrentValues.SetValues(obj: entity);
        return true;
    }

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns><c>true</c> if the user was found and deleted; otherwise, throws a <see cref="UserOperationException"/>.</returns>
    /// <exception cref="UserOperationException">Thrown when the user cannot be found.</exception>
    public async Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        User? user = await DbContext.Set<User>()
            .FirstOrDefaultAsync(predicate: user => user.Guid == guid, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new UserOperationException(message: $"User with ID { guid } not found.");
        }

        DbContext.Set<User>().Remove(entity: user);
        return true;
    }
}
