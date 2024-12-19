using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;

public class ArduinoEntity : BaseEntity
{
    public ArduinoEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, string arduinoId) : base(id, createdUtc,
        modifiedUtc)
    {
        ArduinoId = arduinoId;
    }

    public string ArduinoId { get; set; }
}