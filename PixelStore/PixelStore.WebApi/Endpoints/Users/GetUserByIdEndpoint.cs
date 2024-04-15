using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelStore.Application.Users.GetUserById;
using PixelStore.Application.Users.Shared;
using PixelStore.Domain.Abstractions;
using PixelStore.WebApi.Abstractions;

namespace PixelStore.WebApi.Endpoints.Users;

/// <summary>
/// Endpoint for retrieving a user by their unique identifier (GUID).
/// </summary>
public class GetUserByIdEndpoint : IEndpoint
{
    /// <summary>
    /// Configures the endpoint and maps the corresponding action to a specific HTTP GET route.
    /// </summary>
    /// <param name="app">Defines a class that provides the mechanisms to configure an application’s request pipeline.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "/id/{guid}", handler: HandleRetrieveUserById)
            .WithName(endpointName: "GetUserById")
            .Produces<UserResponseDto>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Handles the HTTP GET request to retrieve a user by their ID.
    /// </summary>
    /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="sender">MediatR sender for handling requests.</param>
    /// <param name="guid">The GUID of the user to retrieve.</param>
    /// <returns>An IResult that contains the result of the handling the request.</returns>
    private async Task<IResult> HandleRetrieveUserById(HttpContext httpContext, [FromServices] ISender sender,
        Guid guid)
    {
        var query = new GetUserByIdQuery(guid: guid);
        Result<UserResponseDto> user = await sender.Send(request: query, cancellationToken: httpContext.RequestAborted);

        return user.IsSuccess ? Results.Ok(value: user.Value) : Results.NotFound();
    }
}
