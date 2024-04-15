using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelStore.Application.Users.GetUsersList;
using PixelStore.Application.Users.Shared;
using PixelStore.Domain.Abstractions;
using PixelStore.WebApi.Abstractions;

namespace PixelStore.WebApi.Endpoints.Users;

/// <summary>
/// Defines the endpoint for retrieving all users.
/// </summary>
public class GetAllUsersEndpoint : IEndpoint
{
    /// <summary>
    /// Maps the "Get All Users" endpoint to the HTTP GET method.
    /// </summary>
    /// <param name="app">Provides the APIs for routing configuration.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "/", handler: HandleRetrieveUsers)
            .WithName(endpointName: "GetAllUsers")
            .Produces<List<UserResponseDto>>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Handles the HTTP GET request to retrieve all users.
    /// </summary>
    /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="sender">MediatR sender for handling requests.</param>
    /// <returns>List that contains the result of handling the request.</returns>
    private async Task<IResult> HandleRetrieveUsers(HttpContext httpContext, [FromServices] ISender sender)
    {
        var query = new GetAllUsersQuery();
        Result<List<UserResponseDto>> users =
            await sender.Send(request: query, cancellationToken: httpContext.RequestAborted);

        return users.IsSuccess ? Results.Ok(value: users.Value) : Results.NotFound();
    }
}
