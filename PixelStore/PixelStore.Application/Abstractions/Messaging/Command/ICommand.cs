using MediatR;
using PixelStore.Domain.Abstractions;

namespace PixelStore.Application.Abstractions.Messaging.Command;

/// <summary>
/// Defines a command interface that includes a specified response type. Commands implementing this interface
/// expect to return a type of <see cref="Result{TResponse}"/>, allowing for operations that include a predictable
/// and strongly typed result. This interface extends from MediatR's IRequest interface, integrating directly
/// with MediatR's handling capabilities.
/// </summary>
/// <typeparam name="TResponse">The type of response expected from the command's execution.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}
