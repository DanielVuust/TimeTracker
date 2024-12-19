using TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TimeRegistration.TimeTracker.Infrastructure.Extensions;
public static class ApplicationBuilderExtension
{
    public static void EnsureDatabaseMigrated(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TimeTrackerContext>();

        context.Database.Migrate();
    }
}