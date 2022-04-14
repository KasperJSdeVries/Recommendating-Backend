using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Repositories;
using Recommendating.Api.Settings;

namespace Recommendating.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ReportApiVersions = true;
            o.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("v"),
                new MediaTypeApiVersionReader("version")
            );
        });

        services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

        services.AddDbContext<DataContext>(
            options => options.UseNpgsql(Configuration
                .GetSection(nameof(PostgreSettings))
                .Get<PostgreSettings>().ConnectionString)
        );

        services.AddScoped<IUserRepository, SqlUserRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}