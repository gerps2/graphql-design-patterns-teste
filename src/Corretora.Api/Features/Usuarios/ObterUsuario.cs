using Corretora.Api.Common;
using Corretora.Api.Database;

namespace Corretora.Api.Features.Usuarios;

public static class ObterUsuario
{
    public sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/web/usuarios/{id:int}", (int id, IBanco banco) =>
                banco.BuscarUsuario(id) is { } usuario
                    ? Results.Ok(usuario)
                    : Results.NotFound());
    }
}
