namespace TimeRegistration.TimeTracker.Domain.TimeTracker;
public class TimeTrackerModel : BaseModel
{
    public TimeTrackerModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, ArduinoModel arduino, DateTime timestamp, string status) : base(id, createdUtc, modifiedUtc)
    {
        Arduino = arduino;
        Timestamp = timestamp;
        Status = status;
    }

    public ArduinoModel Arduino { get; set; }
    public DateTime Timestamp { get; set; }
    public string Status { get; set; }
}
