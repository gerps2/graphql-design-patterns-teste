using AwesomeAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Corretora.IntegrationTests;

public class SchemaTests(WebApplicationFactory<Program> fabrica) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task O_schema_continua_igual_ao_contrato_salvo()
    {
        var cliente = fabrica.CreateClient();

        var schemaAtual = await cliente.GetStringAsync("/graphql?sdl");
        var contrato = await File.ReadAllTextAsync("schema.graphql");

        Normalizar(schemaAtual).Should().Be(Normalizar(contrato));
    }

    private static string Normalizar(string texto) => texto.ReplaceLineEndings("\n").Trim();
}
