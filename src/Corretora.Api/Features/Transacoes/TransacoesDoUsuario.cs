using Corretora.Api.Common;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Transacoes;

[ObjectType<Usuario>]
public static partial class TransacoesDoUsuario
{
    public static Task<List<Transacao>> GetTransacoes([Parent] Usuario usuario, IMediador mediador, CancellationToken ct) =>
        mediador.Enviar(new ObterTransacoes.Query(usuario.Id), ct);
}
