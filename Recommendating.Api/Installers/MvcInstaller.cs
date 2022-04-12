namespace Recommendating.Api.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
    }
}