using Corretora.Api.Common;
using Corretora.Api.Database;

namespace Corretora.Api.Features.Rentabilidade;

public static class ObterRentabilidade
{
    public sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/web/usuarios/{id:int}/rentabilidade", (int id, IBanco banco) =>
                banco.BuscarRentabilidade(id));
    }
}
