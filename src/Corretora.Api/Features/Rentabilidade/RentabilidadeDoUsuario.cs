using Corretora.Api.Database;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Rentabilidade;

[ObjectType<Usuario>]
public static partial class RentabilidadeDoUsuario
{
    public static List<PosicaoRentabilidade> GetRentabilidade([Parent] Usuario usuario, IBanco banco) =>
        banco.BuscarRentabilidade(usuario.Id);
}
