using MediatR;
using PixelStore.Domain.Abstractions;

namespace PixelStore.Application.Abstractions.Messaging.Query;

/// <summary>
/// Represents a query in the CQRS pattern execution pipeline that expects a result.
/// </summary>
/// <typeparam name="TResponse">The type of response that this query returns.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
