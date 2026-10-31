using Corretora.Api.Database;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.Carteira;

[ObjectType<Usuario>]
public static partial class CarteiraDoUsuario
{
    public static List<Posicao> GetCarteira([Parent] Usuario usuario) =>
        BancoEmMemoria.BuscarPosicoes(usuario.Id);
}
