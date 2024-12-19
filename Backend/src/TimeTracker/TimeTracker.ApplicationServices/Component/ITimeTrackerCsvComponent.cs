using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.ApplicationServices.Component;
public interface ITimeTrackerCsvComponent
{
    public IEnumerable<ArduinoLogsModel> UpsertArduinoLogsToCsv(IEnumerable<ArduinoLogsModel> arudinoLogsModel);
}
