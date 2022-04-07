using Recommendating.Api.Repositories;

namespace Recommendating.Api.Installers;

public class RepositoryInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserRepository, InMemUserRepository>();
    }
}