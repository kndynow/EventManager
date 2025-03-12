using EventManager.UI.Configuration;
using Microsoft.Extensions.Options;

namespace EventManager.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();

        // Sets the base address for the EventManager.API.
        builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("ApiSettings"));

        builder.Services.AddScoped(sp =>
        {
            var apiSettings = sp.GetRequiredService<IOptions<ApiConfig>>();
            return new HttpClient { BaseAddress = new Uri(apiSettings.Value.BaseUrl) };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

        app.Run();
    }
}
