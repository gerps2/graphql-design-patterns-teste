using Corretora.Api.Database;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Carteira;

[ObjectType<Usuario>]
public static partial class CarteiraDoUsuario
{
    public static List<PosicaoNaCarteira> GetCarteira([Parent] Usuario usuario, IBanco banco)
    {
        var posicoes = banco.BuscarPosicoes(usuario.Id);
        var total = posicoes.Sum(p => p.Saldo);

        return posicoes
            .Select(p => new PosicaoNaCarteira(p.Ativo, p.Quantidade, p.Saldo, Math.Round(p.Saldo / total * 100, 2)))
            .OrderByDescending(p => p.Participacao)
            .ToList();
    }
}
