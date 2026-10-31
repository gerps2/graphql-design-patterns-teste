using System.Reflection;
using Corretora.Api.Common;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapEndpoints(Assembly.GetExecutingAssembly());

app.Run();
