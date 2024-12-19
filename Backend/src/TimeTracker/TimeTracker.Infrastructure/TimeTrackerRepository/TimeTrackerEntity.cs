using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;

public class TimeTrackerEntity : BaseEntity
{
    public TimeTrackerEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, ArduinoEntity arduino, DateTime timestamp, string status) : base(id, createdUtc, modifiedUtc)
    {
        Arduino = arduino;
        Timestamp = timestamp;
        Status = status;
    }

    public ArduinoEntity Arduino { get; set; }
    public DateTime Timestamp { get; set; }
    public string Status { get; set; }
}