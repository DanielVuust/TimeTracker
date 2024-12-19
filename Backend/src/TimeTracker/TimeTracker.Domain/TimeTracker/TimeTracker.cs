namespace TimeRegistration.TimeTracker.Domain.TimeTracker;
public class TimeTrackerModel : BaseModel
{
    public TimeTrackerModel(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}
