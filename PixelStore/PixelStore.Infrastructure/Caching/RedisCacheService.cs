using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using PixelStore.Application.Abstractions.Caching;

namespace PixelStore.Infrastructure.Caching;

/// <summary>
/// Provides methods for caching operations backed by Redis.
/// </summary>
internal sealed class RedisCacheService : ICacheService
{
    private const int DefaultExpirationTimeInMinutes = 5;
    private readonly IDistributedCache _redisCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisCacheService"/> with the specified distributed cache.
    /// </summary>
    /// <param name="cache">The distributed cache which is typically provided by Redis.</param>
    public RedisCacheService(IDistributedCache cache)
    {
        ArgumentNullException.ThrowIfNull(cache);
        _redisCache = cache;
    }
    
    /// <summary>
    /// Asynchronously retrieves an item from the cache and deserializes it to the specified type.
    /// </summary>
    /// <param name="key">The key identifying the item in the cache.</param>
    /// <param name="cancellationToken">Optional. The token to monitor for cancellation requests.</param>
    /// <typeparam name="T">The type of the item stored in the cache.</typeparam>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized item,
    /// or default if not found.</returns>
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? cachedData = await _redisCache.GetAsync(key: key, token: cancellationToken);
        return cachedData is null ? default : JsonSerializer.Deserialize<T>(utf8Json:cachedData);
    }

    /// <summary>
    /// Asynchronously adds an item to the cache with optional expiration time.
    /// </summary>
    /// <param name="key">The key under which to store the item.</param>
    /// <param name="value">The item to be stored.</param>
    /// <param name="expirationTime">Optional. The relative time from now when the item should expire.</param>
    /// <param name="cancellationToken">Optional. The token to monitor for cancellation requests.</param>
    /// <typeparam name="T">The type of the item to store in the cache.</typeparam>
    public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromMinutes(DefaultExpirationTimeInMinutes)
        };

        byte[] jsonData = JsonSerializer.SerializeToUtf8Bytes(value: value);
        await _redisCache.SetAsync(key: key, value: jsonData, options: options, token: cancellationToken);
    }

    /// <summary>
    /// Asynchronously removes the item associated with the specified key from the cache.
    /// </summary>
    /// <param name="key">The key identifying the item to remove.</param>
    /// <param name="cancellationToken">Optional. The token to monitor for cancellation requests.</param>
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _redisCache.RemoveAsync(key: key, token: cancellationToken);
    }
}
