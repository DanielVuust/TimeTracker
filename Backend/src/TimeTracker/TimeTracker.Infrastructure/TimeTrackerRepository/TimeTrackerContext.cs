using TimeRegistration.TimeTracker.Infrastructure.OperationRepository;
using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
public class TimeTrackerContext : DbContext
{
    public TimeTrackerContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OperationEntity>? Operations { get; set; }
    public DbSet<TimeTrackerEntity>? TimeTrackers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OperationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TimeTrackerConfiguration());
    }
}
