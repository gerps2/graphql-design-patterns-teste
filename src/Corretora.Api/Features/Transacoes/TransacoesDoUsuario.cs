using Corretora.Api.Database;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Transacoes;

[ObjectType<Usuario>]
public static partial class TransacoesDoUsuario
{
    public static List<Transacao> GetTransacoes([Parent] Usuario usuario, IBanco banco) =>
        banco.BuscarTransacoes(usuario.Id)
            .OrderByDescending(t => t.Data)
            .ToList();
}
