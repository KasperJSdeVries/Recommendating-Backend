using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Repositories;
using Recommendating.Api.Settings;

namespace Recommendating.Api.Installers;

public class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(
            options => options.UseNpgsql(configuration
                .GetSection(nameof(PostgreSettings))
                .Get<PostgreSettings>().ConnectionString)
        );

        services.AddScoped<IUserRepository, SqlUserRepository>();
    }
}