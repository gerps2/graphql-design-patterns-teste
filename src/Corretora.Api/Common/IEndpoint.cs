using System.Reflection;

namespace Corretora.Api.Common;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder app);
}

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app, Assembly assembly)
    {
        var endpoints = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(IEndpoint)))
            .Select(t => (IEndpoint)Activator.CreateInstance(t)!);

        foreach (var endpoint in endpoints)
            endpoint.Map(app);

        return app;
    }
}
