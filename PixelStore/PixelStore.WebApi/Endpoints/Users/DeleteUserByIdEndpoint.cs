using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelStore.Application.Users.DeleteUserById;
using PixelStore.Domain.Abstractions;
using PixelStore.WebApi.Abstractions;

namespace PixelStore.WebApi.Endpoints.Users;

/// <summary>
/// Endpoint for deleting a user by their unique identifier (GUID).
/// </summary>
public class DeleteUserByIdEndpoint : IEndpoint
{
    /// <summary>
    /// Configures the endpoint and maps the corresponding action to a specific HTTP DELETE route.
    /// </summary>
    /// <param name="app">The endpoint route builder used to configure API routes.</param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(pattern: "/id/{guid}", handler: HandleDeleteUserById)
            .WithName(endpointName: "DeleteUserById")
            .Produces<Result<bool>>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Handles the HTTP DELETE request to remove a user by their ID.
    /// </summary>
    /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="sender">MediatR sender used to send the delete command to the appropriate handler.</param>
    /// <param name="guid">The GUID of the user to delete.</param>
    /// <returns>An IResult that contains the result of the handling the request.</returns>
    private async Task<IResult> HandleDeleteUserById(HttpContext httpContext, [FromServices] ISender sender, Guid guid)
    {
        DeleteUserByIdCommand command = new DeleteUserByIdCommand(UserId: guid);
        Result<bool> result = await sender.Send(request: command, cancellationToken: httpContext.RequestAborted);

        return result.IsSuccess ? Results.Ok() : Results.NotFound();
    }
}
