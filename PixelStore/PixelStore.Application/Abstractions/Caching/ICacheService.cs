namespace PixelStore.Application.Abstractions.Caching;

/// <summary>
/// Defines a standard interface for caching, providing methods to get, set, and remove cache entries.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Asynchronously retrieves the cached entry if it exists.
    /// </summary>
    /// <param name="key">The unique key for the cached item.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <typeparam name="T">The type of the cached item.</typeparam>
    /// <returns>The cached item if found, or null otherwise.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds an item to the cache with an absolute expiration time.
    /// </summary>
    /// <param name="key">The unique key for the cached item.</param>
    /// <param name="value">The item to be cached.</param>
    /// <param name="expirationTime">The time at which the cached entry expires.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <typeparam name="T">The type of the item to cache.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously removes the cached entry if it exists.
    /// </summary>
    /// <param name="key">The unique key for the cached item.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation of removing the cached item.</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
