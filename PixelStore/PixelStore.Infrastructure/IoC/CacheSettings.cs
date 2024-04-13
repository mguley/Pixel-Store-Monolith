namespace PixelStore.Infrastructure.IoC;

/// <summary>
/// Provides configuration settings for cache implementations, particularly Redis.
/// </summary>
public class CacheSettings
{
    /// <summary>
    /// Gets the Redis connection string configuration which is necessary to establish connectivity with
    /// the Redis cache instance.
    /// </summary>
    public string RedisConfiguration { get; init; } = string.Empty;
}
