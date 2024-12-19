namespace TimeRegistration.TimeTracker.Domain.ArduinoLogs;
public sealed class LogsModel
{
    public DateTime Timestamp { get; }
    public string Status { get; }
    
    public LogsModel(DateTime timestamp, string status)
    {
        Timestamp = timestamp;
        Status = status;
    }
}
