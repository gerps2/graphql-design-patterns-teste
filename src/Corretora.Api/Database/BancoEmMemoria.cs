using Corretora.Api.Features.Carteira;
using Corretora.Api.Features.Rentabilidade;
using Corretora.Api.Features.Transacoes;
using Corretora.Api.Features.Usuarios;

namespace Corretora.Api.Database;

public static class BancoEmMemoria
{
    public static readonly List<Usuario> Usuarios =
    [
        new(1, "Ana Souza", "ana@corretora.com", "11 99999-0001", "123.456.789-00",
            new Endereco("Rua das Flores", 10, "São Paulo")),
        new(2, "Bruno Lima", "bruno@corretora.com", "11 99999-0002", "987.654.321-00",
            new Endereco("Av. Paulista", 1000, "São Paulo")),
    ];

    public static readonly List<Posicao> Posicoes =
    [
        new(1, "PETR4", 100, 3850.00m),
        new(1, "ITUB4", 50, 1620.00m),
        new(1, "HGLG11", 10, 1590.00m),
        new(2, "VALE3", 30, 1830.00m),
    ];

    public static readonly List<Transacao> Transacoes =
    [
        new(1, 1, "PETR4", "Compra", 1925.00m, new DateOnly(2026, 10, 20)),
        new(1, 2, "ITUB4", "Compra", 810.00m, new DateOnly(2026, 10, 22)),
        new(1, 3, "HGLG11", "Venda", 318.00m, new DateOnly(2026, 10, 28)),
        new(2, 4, "VALE3", "Compra", 1830.00m, new DateOnly(2026, 10, 25)),
    ];

    public static readonly List<PosicaoRentabilidade> Rentabilidades =
    [
        new(1, "PETR4", 2.4m),
        new(1, "ITUB4", 1.1m),
        new(1, "HGLG11", 0.7m),
        new(2, "VALE3", -0.8m),
    ];
}
