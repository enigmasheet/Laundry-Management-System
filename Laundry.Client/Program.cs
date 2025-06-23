using Laundry.Client.Authentication;
using Laundry.Client.Services;
using Laundry.Client.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Laundry.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Register your custom AuthenticationStateProvider once
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IVendorService, VendorService>();

            // Add Blazor authorization services
            builder.Services.AddAuthorizationCore();

            // Register the message handler for JWT token injection
            builder.Services.AddTransient<JwtAuthorizationMessageHandler>();

            // Configure HttpClient with the message handler
            builder.Services.AddHttpClient("AuthorizedClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress);
            })
            .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

            // Register HttpClient for injection, uses the AuthorizedClient config
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));

            await builder.Build().RunAsync();
        }
    }
}
