using Corretora.Api.Common;
using Corretora.Api.Database;

namespace Corretora.Api.Features.Transacoes;

public static class ObterTransacoes
{
    public sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/web/usuarios/{id:int}/transacoes", (int id, IBanco banco) =>
                banco.BuscarTransacoes(id)
                    .OrderByDescending(t => t.Data)
                    .ToList());
    }
}
