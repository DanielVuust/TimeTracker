using TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;
using TimeRegistration.TimeTracker.Domain.TimeTracker;
using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
public class TimeTrackerRepository : BaseRepository<TimeTrackerModel, TimeTrackerEntity>, ITimeTrackerRepository
{
    public TimeTrackerRepository(TimeTrackerContext context) : base(context)
    {
    }

    private DbSet<TimeTrackerEntity> GetUserDbSet()
    {
        if (Context.TimeTrackers is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.TimeTrackers;
    }

    protected override TimeTrackerModel Map(TimeTrackerEntity entity)
    {
        return TimeTrackerEntityMapper.Map(entity);
    }

    protected override TimeTrackerEntity Map(TimeTrackerModel model)
    {
        return TimeTrackerEntityMapper.Map(model);
    }
}
