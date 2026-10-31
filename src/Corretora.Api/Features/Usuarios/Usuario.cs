namespace Corretora.Api.Features.Usuarios;

public record Endereco(string Rua, int Numero, string Cidade);

public record Usuario(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    [property: GraphQLIgnore] string Cpf,
    Endereco Endereco);
