using Microsoft.AspNetCore.Builder;
using Serilog;

namespace FacileBudget
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            });

            Startup startup = new(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            WebApplication app = builder.Build();

            startup.Configure(app);

            app.Run();
        }
    }
}