using Corretora.Api.Database;
using Corretora.Api.Features.Carteira;
using Corretora.Api.Features.Rentabilidade;
using Corretora.Api.Features.Transacoes;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Features.TelaInicial;

public static class ObterTelaInicial
{
    public record UsuarioCompleto(
        int Id,
        string Nome,
        string Email,
        string Telefone,
        string Cpf,
        Endereco Endereco,
        List<Posicao> Carteira,
        List<Transacao> Transacoes,
        List<PosicaoRentabilidade> Rentabilidade);

    public class Query
    {
        public UsuarioCompleto? GetUsuario(int id)
        {
            var usuario = BancoEmMemoria.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null) return null;

            return new UsuarioCompleto(
                usuario.Id, usuario.Nome, usuario.Email, usuario.Telefone, usuario.Cpf, usuario.Endereco,
                BancoEmMemoria.Posicoes.Where(p => p.UsuarioId == id).ToList(),
                BancoEmMemoria.Transacoes.Where(t => t.UsuarioId == id).ToList(),
                BancoEmMemoria.Rentabilidades.Where(r => r.UsuarioId == id).ToList());
        }
    }
}
