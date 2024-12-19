using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;

public class TimeTrackerEntity : BaseEntity
{
    public TimeTrackerEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, Guid arduinoId, DateTime timestamp, string status) : base(id, createdUtc, modifiedUtc)
    {
        ArduinoId = arduinoId;
        Timestamp = timestamp;
        Status = status;
    }

    public Guid ArduinoId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Status { get; set; }
}