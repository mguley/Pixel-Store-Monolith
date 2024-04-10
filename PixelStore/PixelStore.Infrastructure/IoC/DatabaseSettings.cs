namespace PixelStore.Infrastructure.IoC;

/// <summary>
/// Represents the database settings used for configuring the application's data access layer.
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// Gets or sets the default connection type to be used by the application.
    /// This setting is used to determine which database provider to use.
    /// </summary>
    public string DefaultConnection { get; init; } = string.Empty;
}
