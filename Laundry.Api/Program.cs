using Microsoft.EntityFrameworkCore;
using Serilog;
using Laundry.Api.Data; // Replace with your actual DbContext namespace

namespace Laundry.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog for console logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Use Serilog as the logging provider
            builder.Host.UseSerilog();

            // Add EF Core DbContext (replace "DefaultConnection" with your connection string key)
            builder.Services.AddDbContext<LaundryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers and swagger services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Laundry API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            // Enable Swagger UI in Development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // No authentication middleware since JWT not implemented yet
            // app.UseAuthentication();  // Not added
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
