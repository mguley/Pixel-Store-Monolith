namespace PixelStore.WebApi.Abstractions;

/// <summary>
/// Defines a contract for mapping an endpoint to the ASP.NET Core routing system.
/// This interface is used to implement classes that specifically handle the mapping of HTTP request endpoints
/// to methods that execute the request logic.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Maps the current endpoint to the application's routing mechanism.
    /// </summary>
    /// <param name="app">The endpoint route builder that provides the APIs needed to configure endpoint routing.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}
