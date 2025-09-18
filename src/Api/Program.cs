using Api.GraphQL;
using Api.Models;
using Api.Services;
using Api.Services.Abstractions;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Data;
using ImTools;
using JasperFx;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

// ---------- Marten ----------
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Default") ??
                       "Host=localhost;Database=well_euled;Username=postgres;Password=postgres");
    options.AutoCreateSchemaObjects = AutoCreate.All;
});

// ---------- Wolverine ----------
builder.Host.UseWolverine();

// ---------- HotChocolate GraphQL ----------
// GraphQL setup
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Api.GraphQL.Queries.CalculationQuery>()
    .AddMutationType<Api.GraphQL.Mutations.CalculationMutation>();

// ---------- Other services ----------
builder.Services.AddScoped<ICalculationService, CalculationService>();

var app = builder.Build();

app.MapGraphQL(); // GraphQL endpoint at /graphql

app.MapGet("/", () => "Well-Euled Machine API is running!");

app.Run();