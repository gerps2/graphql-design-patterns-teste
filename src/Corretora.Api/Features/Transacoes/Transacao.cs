namespace Corretora.Api.Features.Transacoes;

public enum TipoTransacao
{
    Compra,
    Venda,
}

public record Transacao([property: GraphQLIgnore] int UsuarioId, int Id, string Ativo, TipoTransacao Tipo, decimal Valor, DateOnly Data);
