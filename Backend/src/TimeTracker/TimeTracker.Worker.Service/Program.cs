using TimeRegistration.TimeTracker.Infrastructure.Installers;
using TimeRegistration.TimeTracker.Infrastructure.Extensions;
using TimeRegistration.TimeTracker.Infrastructure.Startup;
using Microsoft.AspNetCore;

namespace TimeRegistration.TimeTracker.Worker.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
         var host = CreateWebHostBuilder(args).Build();

            var runOnStartupExecution = host.Services.GetRequiredService<IRunOnStartupExecution>();

            await runOnStartupExecution.RunAll();
            await host.RunAsync();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration(builder =>
               {
                   builder
                       .AddEnvironmentVariables()
                       .Build();
               })
               .ConfigureLogging((hostingContext, logging) =>
               {
                   logging.AddConsole();
               })
               .ConfigureServices((context, collection) =>
               {
                   var configuration = context.Configuration;

                   var parameters = new DependencyInstallerOptions(configuration, context.HostingEnvironment);
                   collection.AddFromAssembly<Program>(parameters);
                   collection.AddFromAssembly<ServiceInstaller>(parameters);
               })
               .UseStartup<Startup>();
}
