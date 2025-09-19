using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRadzenComponents();


// Add Radzen.Blazor services
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenQueryStringThemeService();


await builder.Build().RunAsync();
