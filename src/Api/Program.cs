using Marten;
using Wolverine;
using Wolverine.Marten;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.GraphQL;
using Api.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------- Marten ----------
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Default") ??
                       "Host=localhost;Database=well_euled;Username=postgres;Password=postgres");
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
});

// ---------- Wolverine ----------
builder.Host.UseWolverine();

// ---------- HotChocolate GraphQL ----------
builder.Services
    .AddGraphQLServer()
    .AddQueryType<CalculationQuery>();

// ---------- Other services ----------
// e.g., Add validation, event handlers, etc.

var app = builder.Build();

app.MapGraphQL(); // GraphQL endpoint at /graphql

app.MapGet("/", () => "Well-Euled Machine API is running!");

app.Run();