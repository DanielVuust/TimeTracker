using TimeRegistration.TimeTracker.Domain.TimeTracker;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
internal static class TimeTrackerEntityMapper
{
    internal static TimeTrackerEntity Map(TimeTrackerModel model)
    {
        return new TimeTrackerEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc,
            ArduinoEntityMapper.Map(model.Arduino),
            model.Timestamp,
            model.Status
        );
    }

    internal static TimeTrackerModel Map(TimeTrackerEntity entity)
    {
        return new TimeTrackerModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc,
            ArduinoEntityMapper.Map(entity.Arduino),
            entity.Timestamp,
            entity.Status
        );
    }
}
