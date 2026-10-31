using System.Net.Http.Json;
using System.Text.Json;
using AwesomeAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Corretora.IntegrationTests;

public class RegraDeLeituraTests(WebApplicationFactory<Program> fabrica) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task A_maior_posicao_vem_primeiro_com_a_participacao_calculada()
    {
        var cliente = fabrica.CreateClient();
        var pedido = new { query = "{ usuario(id: 1) { carteira { ativo participacao } } }" };

        var resposta = await cliente.PostAsJsonAsync("/graphql", pedido);
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        var primeira = json.GetProperty("data").GetProperty("usuario").GetProperty("carteira")[0];
        primeira.GetProperty("ativo").GetString().Should().Be("PETR4");
        primeira.GetProperty("participacao").GetDecimal().Should().Be(54.53m);
    }
}
