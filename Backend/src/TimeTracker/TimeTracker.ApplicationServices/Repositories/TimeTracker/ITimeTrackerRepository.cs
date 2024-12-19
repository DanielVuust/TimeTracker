using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;
public interface ITimeTrackerRepository : IBaseRepository<ArduinoLogsModel>
{
    Task<IEnumerable<ArduinoLogsModel?>> GetArduinoLogsByArduinoId(Guid arduinoId);
}
