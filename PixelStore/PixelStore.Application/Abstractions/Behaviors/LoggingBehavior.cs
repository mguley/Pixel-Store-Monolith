using MediatR;
using PixelStore.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace PixelStore.Application.Abstractions.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that logs the execution of requests.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public sealed class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IBaseRequest 
    where TResponse : IDomainResult
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging request details.</param>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }
    
    /// <summary>
    /// Intercepts the handling of a request at the pipeline level, logs the start and the result of the request handling.
    /// </summary>
    /// <param name="request">The request being handled.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the request handling.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        _logger.LogInformation("Starting request processing for {RequestName}", requestName);
        try
        {
            TResponse response = await next();
            
            if (response.IsSuccess)
            {
                _logger.LogInformation("Request {RequestName} processed successfully.", requestName);
            }
            else
            {
                _logger.LogError("Request {RequestName} processed with error: {Error}", requestName,
                    response.GetErrorMessage);
            }
            
            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception: exception,
                message: "Request {RequestName} processing failed due to an exception.", requestName);
            throw;
        }
    }
}
