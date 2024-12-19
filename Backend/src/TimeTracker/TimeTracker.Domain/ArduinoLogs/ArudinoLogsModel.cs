using System.Text.Json.Serialization;

namespace TimeRegistration.TimeTracker.Domain.ArduinoLogs;
public class ArduinoLogsModel : BaseModel
{
    public Guid ArduinoId { get; set; }
    public LogsModel LogsModel { get; }

    public ArduinoLogsModel(Guid id, DateTime createdUtc, DateTime modifiedUtc)
       : base(id, createdUtc, modifiedUtc)
    {
    }

    public ArduinoLogsModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, Guid arduinoId, LogsModel logsModel)
        : base(id, createdUtc, modifiedUtc)
    {
        ArduinoId = arduinoId;
    }

    public static ArduinoLogsModel Create(Guid arduinoid, DateTime timestamp, string status)
    {
        return new ArduinoLogsModel(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, arduinoid, new LogsModel(timestamp, status));
    }
}
