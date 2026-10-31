using System.Reflection;
using Corretora.Api.Common;
using Corretora.Api.Features.TelaInicial;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<ObterTelaInicial.Query>();

var app = builder.Build();

app.MapEndpoints(Assembly.GetExecutingAssembly());
app.MapGraphQL();

app.Run();
