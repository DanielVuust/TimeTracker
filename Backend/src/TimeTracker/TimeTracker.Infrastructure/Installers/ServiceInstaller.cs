using Microsoft.Extensions.DependencyInjection;
using TimeRegistration.TimeTracker.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeRegistration.TimeTracker.ApplicationServices.Repositories.Operations;
using TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.Infrastructure.TimeTrackerCsv;
using TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;

namespace TimeRegistration.TimeTracker.Infrastructure.Installers;

public sealed class ServiceInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection, DependencyInstallerOptions options)
    {
        serviceCollection.AddTransient<IRunOnStartupExecution, RunOnStartupExecution>();
        serviceCollection.AddTransient<ITimeTrackerCsvComponent, TimeTrackerCsvComponent>();
        AddRepositories(serviceCollection, options.Configuration);
    }

    private static void AddRepositories(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration[Constants.ConfigurationKeys.SqlDbConnectionString];

        serviceCollection.AddDbContext<TimeTrackerContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddScoped<ITimeTrackerRepository, TimeTrackerRepository.TimeTrackerRepository>();
        serviceCollection.AddScoped<IOperationRepository, OperationRepository.OperationRepository>();
    }
}

