namespace Corretora.Api.Features.Transacoes;

public record Transacao(int UsuarioId, int Id, string Ativo, string Tipo, decimal Valor, DateOnly Data);
