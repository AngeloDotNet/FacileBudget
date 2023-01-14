namespace FacileBudget;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddResponseCaching();
        services.AddControllersWithViews();

        services.AddValidatorsFromAssemblyContaining<SpeseCreateValidator>();
        services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;
        });

        // Database
        services.AddTransient<ISpeseService, AdoNetSpeseService>();
        services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();

        // Options
        services.Configure<SpeseOptions>(Configuration.GetSection("Spese"));
        services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        if (env.IsEnvironment("Development"))
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });

        app.UseStaticFiles();
        app.UseRouting();

        app.UseResponseCaching();
        app.UseEndpoints(routeBuilder =>
        {
            routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });
    }
}