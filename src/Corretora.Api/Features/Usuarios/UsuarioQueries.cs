using Corretora.Api.Database;

namespace Corretora.Api.Features.Usuarios;

[QueryType]
public static partial class UsuarioQueries
{
    public static Usuario? GetUsuario(int id) =>
        BancoEmMemoria.BuscarUsuario(id);
}
