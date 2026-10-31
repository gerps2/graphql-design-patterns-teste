using Corretora.Api.Common;
using Corretora.Api.Database;

namespace Corretora.Api.Features.Carteira;

public static class ObterCarteira
{
    public sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/web/usuarios/{id:int}/carteira", (int id) =>
                BancoEmMemoria.Posicoes.Where(p => p.UsuarioId == id));
    }
}
