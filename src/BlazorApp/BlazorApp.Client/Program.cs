using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorApp;
using BlazorApp.Client.GraphQL;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services
    .AddHttpClient("GraphQLClient", c => c.BaseAddress = new Uri("https://localhost:5000/graphql"));


builder.Services.AddWellEuledClient();
builder.Services.AddScoped<ExecuteGapMutation>();


await builder.Build().RunAsync();
