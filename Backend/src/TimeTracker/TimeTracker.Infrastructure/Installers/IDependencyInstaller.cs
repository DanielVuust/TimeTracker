using Microsoft.Extensions.DependencyInjection;

namespace TimeRegistration.TimeTracker.Infrastructure.Installers;
public interface IDependencyInstaller
{
    void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options);
}