using Corretora.Api.Common;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Carteira;

[ObjectType<Usuario>]
public static partial class CarteiraDoUsuario
{
    public static Task<List<PosicaoNaCarteira>> GetCarteira([Parent] Usuario usuario, IMediador mediador, CancellationToken ct) =>
        mediador.Enviar(new ObterCarteira.Query(usuario.Id), ct);
}
