namespace Corretora.Api.Features.Carteira;

public record Posicao([property: GraphQLIgnore] int UsuarioId, string Ativo, decimal Quantidade, decimal Saldo);
