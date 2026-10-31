namespace Corretora.Api.Features.Rentabilidade;

public record PosicaoRentabilidade([property: GraphQLIgnore] int UsuarioId, string Ativo, decimal PercentualMes);
