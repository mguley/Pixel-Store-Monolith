namespace PixelStore.Domain.Abstractions;

/// <summary>
/// Represents the base class for all entities in the domain model.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    protected int Id { get; init; }
}
