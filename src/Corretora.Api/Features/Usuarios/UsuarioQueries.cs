using Corretora.Api.Common;

namespace Corretora.Api.Features.Usuarios;

[QueryType]
public static partial class UsuarioQueries
{
    public static Task<Usuario?> GetUsuario(int id, IMediador mediador, CancellationToken ct) =>
        mediador.Enviar(new ObterUsuario.Query(id), ct);

    public static Task<List<Usuario>> GetUsuarios(IMediador mediador, CancellationToken ct) =>
        mediador.Enviar(new ListarUsuarios.Query(), ct);
}
