using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
public class TimeTrackerConfiguration : BaseEntityConfiguration<TimeTrackerEntity>
{
    public override void Configure(EntityTypeBuilder<TimeTrackerEntity> builder)
    {
        base.Configure(builder);

        builder.HasIndex(builder => builder.ArduinoId);

        builder.Property(e => e.ArduinoId).IsRequired();
        builder.Property(e => e.Timestamp).IsRequired();
        builder.Property(e => e.Status).IsRequired();
    }
}