namespace PixelStore.Application.Abstractions.Messaging.Command;

/// <summary>
/// Serves as a marker interface for all command types within the application. This interface is used to
/// identify classes that are intended to perform actions and optionally return a result as part of CQRS operations.
/// Utilizing this interface allows for the application of cross-cutting concerns, such as logging, validation,
/// and transaction management, consistently across all command handlers.
/// </summary>
public interface IBaseCommand
{
}
