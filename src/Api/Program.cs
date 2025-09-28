using Api.GraphQL;
using Api.GraphQL.Mutations;
using Api.Handlers;
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
using Api.GraphQL.Queries;

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
    .ModifyOptions(opt => opt.EnableDirectiveIntrospection = true)
    .AddApiTypes();

// ---------- Other services ----------
builder.Services.AddScoped<ICalculationService, CalculationService>();


// ---------- GAP Kernel Service ----------
builder.Services.AddHttpClient<IGapKernelClient, GapKernelClient>(client =>
{
    client.BaseAddress = new Uri("http://jupyter-gap:8888");
});
// Single shared instance
builder.Services.AddSingleton<IGapKernelClient, GapKernelClient>();


var app = builder.Build();

app.MapGraphQL(); // GraphQL endpoint at /graphql

app.MapGet("/", () => "Well-Euled Machine API is running!");

app.MapPost("/gap", async (IMessageBus bus, string code) =>
{
    var result = await bus.InvokeAsync<GapResult>(new ExecuteGapCommand(code));
    return Results.Ok(result);
});

app.MapPost("/gap/execute", async (IMessageBus bus, ExecuteGapCommand cmd) =>
{
    var result = await bus.InvokeAsync<GapResult>(cmd);
    return Results.Ok(result);
});


app.Run();