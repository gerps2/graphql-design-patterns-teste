using System.Reflection;
using System.Text.Json.Serialization;
using Corretora.Api.Common;

[assembly: Module("Corretora")]

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services
    .AddGraphQLServer()
    .AddCorretora();

var app = builder.Build();

app.MapEndpoints(Assembly.GetExecutingAssembly());
app.MapGraphQL();

app.Run();
