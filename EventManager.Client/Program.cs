using EventManager.Client;
using EventManager.Client.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("ApiSettings"));

// builder.Services.AddScoped(sp =>
// {
//     var apiSettings = sp.GetRequiredService<IOptions<ApiConfig>>();
//     return new HttpClient { BaseAddress = new Uri(apiSettings.Value.BaseUrl) };
// });

await builder.Build().RunAsync();
