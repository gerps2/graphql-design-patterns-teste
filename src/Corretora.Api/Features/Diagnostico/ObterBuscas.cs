using Corretora.Api.Common;
using Corretora.Api.Database;

namespace Corretora.Api.Features.Diagnostico;

// Apoio da aula: devolve quantas buscas aconteceram e zera o contador.
public static class ObterBuscas
{
    public sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/diagnostico/buscas", () => new { buscas = BancoEmMemoria.LerEZerarBuscas() });
    }
}
