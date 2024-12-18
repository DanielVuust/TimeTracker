using TimeRegistration.TimeTracker.Infrastructure.Installers;

namespace TimeRegistration.TimeTracker.Worker.Service.Installers
{
    public class ServiceInstaller : IDependencyInstaller
    {
        public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
        {
            //Add service dependencies
        }
    }
}