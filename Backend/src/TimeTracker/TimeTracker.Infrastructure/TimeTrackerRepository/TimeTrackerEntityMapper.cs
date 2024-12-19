using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
internal static class TimeTrackerEntityMapper
{
    internal static TimeTrackerEntity Map(ArduinoLogsModel model)
    {
        return new TimeTrackerEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc
            );
    }

    internal static ArduinoLogsModel Map(TimeTrackerEntity entity)
    {
        return new ArduinoLogsModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc
            );
    }
}
