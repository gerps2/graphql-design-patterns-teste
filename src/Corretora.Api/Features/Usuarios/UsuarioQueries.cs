using Corretora.Api.Database;

namespace Corretora.Api.Features.Usuarios;

[QueryType]
public static partial class UsuarioQueries
{
    public static Usuario? GetUsuario(int id, IBanco banco) =>
        banco.BuscarUsuario(id);

    public static List<Usuario> GetUsuarios(IBanco banco) =>
        banco.BuscarUsuarios();
}
