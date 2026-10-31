using Corretora.Api.Common;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Rentabilidade;

[ObjectType<Usuario>]
public static partial class RentabilidadeDoUsuario
{
    public static Task<List<PosicaoRentabilidade>> GetRentabilidade([Parent] Usuario usuario, IMediador mediador, CancellationToken ct) =>
        mediador.Enviar(new ObterRentabilidade.Query(usuario.Id), ct);
}
