using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PixelStore.WebApi.Abstractions;

namespace PixelStore.WebApi.Extensions;

/// <summary>
/// Provides extension methods for registering and mapping API endpoints.
/// </summary>
public static class EndpointExtensions
{
    private const string Namespace = "PixelStore.WebApi.Endpoints";
    
    /// <summary>
    /// Adds endpoint services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="assembly">The assembly to scan for endpoints.</param>
    /// <returns>The original IServiceCollection for chaining.</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && 
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();
        services.TryAddEnumerable(descriptors: serviceDescriptors);
        
        return services;
    }

    /// <summary>
    /// Maps all registered IEndpoint instances to the application's routing mechanism.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
    /// <param name="routeGroupBuilder">Optional RouteGroupBuilder for grouping routes.</param>
    /// <returns>The original WebApplication for chaining.</returns>
    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;
        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }

    /// <summary>
    /// Dynamically maps endpoints to their route groups based on the namespace.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
    /// <param name="assembly">The assembly containing the endpoint implementations.</param>
    /// <param name="baseNamespace">The base namespace for grouping.</param>
    public static void MapDynamicEndpoints(this WebApplication app, Assembly assembly, string baseNamespace = Namespace) 
    {
        IEnumerable<Type> endpointTypes = assembly.GetTypes()
            .Where(type => type is { IsInterface: false, IsAbstract: false } &&
                           typeof(IEndpoint).IsAssignableFrom(type));

        var endpointsByGroup = endpointTypes
            .GroupBy(type => type.Namespace?
                .Replace(oldValue: baseNamespace, newValue: "")
                .Trim(trimChar: '.')
                .Split(separator: '.')
                .First());

        foreach (IGrouping<string?, Type> group in endpointsByGroup)
        {
            var routeGroup = app.MapGroup($"/{group?.Key?.ToLower()}");
            if (group is not null)
            {
                foreach (var endpointType in group)
                {
                    var endpointInstance = (IEndpoint)Activator.CreateInstance(endpointType)!;
                    endpointInstance.MapEndpoint(routeGroup);
                }
            }
        }
    }
}
