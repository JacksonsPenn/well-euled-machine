using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorApp;
//using BlazorApp.GraphQL;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services
    .AddHttpClient("GraphQLClient", c => c.BaseAddress = new Uri("https://localhost:5000/graphql"));
    //.AddGraphQLClient();


await builder.Build().RunAsync();
