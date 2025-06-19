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



            builder.Services.AddScoped(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                
            
                return new HttpClient
                {
                    BaseAddress = new Uri(config["ApiBaseUrl"]!)
                };
            });





            await builder.Build().RunAsync();
        }
    }
}
