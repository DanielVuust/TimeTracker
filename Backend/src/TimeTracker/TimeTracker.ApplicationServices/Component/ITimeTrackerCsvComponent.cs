using System.Text.Json.Serialization;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.ApplicationServices.Component;
public interface ITimeTrackerCsvComponent
{
    public bool UpsertArduinoLogsToCsv(IEnumerable<ArduinoLog> logs, string arduinoId);
    public IEnumerable<ArduinoLog> GetArduinoLogsFromCsv(string arduinoId);
}

public class ArduinoLog
{
    public string Status { get; set; }
    public DateTime Timestamp { get; set; }
}
