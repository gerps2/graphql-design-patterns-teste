using Corretora.Api.Common;
using Corretora.Api.Database;

namespace Corretora.Api.Features.Carteira;

public static class ObterCarteira
{
    public sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/web/usuarios/{id:int}/carteira", (int id, IBanco banco) =>
            {
                var posicoes = banco.BuscarPosicoes(id);
                var total = posicoes.Sum(p => p.Saldo);

                return posicoes
                    .Select(p => new PosicaoNaCarteira(p.Ativo, p.Quantidade, p.Saldo, Math.Round(p.Saldo / total * 100, 2)))
                    .OrderByDescending(p => p.Participacao)
                    .ToList();
            });
    }
}
