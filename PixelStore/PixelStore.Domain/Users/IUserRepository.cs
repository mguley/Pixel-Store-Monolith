using PixelStore.Domain.Abstractions;

namespace PixelStore.Domain.Users;

/// <summary>
/// Represents the repository interface for user entity.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Asynchronously determines whether any user entities exist in the repository.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The task result contains 'true' if any entities exist; otherwise, 'false'.</returns>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
