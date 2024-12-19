using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;

public class ArduinoConfiguration : BaseEntityConfiguration<ArduinoEntity>
{
    public override void Configure(EntityTypeBuilder<ArduinoEntity> builder)
    {
        base.Configure(builder);
    }
}