using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebApp;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/graphql") });
builder.Services.AddMudServices();
// Register StrawberryShake GraphQL client
//builder.Services.AddWellEuledClient()
//  .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5000/graphql"));

await builder.Build().RunAsync();