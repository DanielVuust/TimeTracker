//using TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;
//using TimeRegistration.TimeTracker.Domain.ArduinoLogs;
//using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;
//using Microsoft.EntityFrameworkCore;

//namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
//public class TimeTrackerRepository : BaseRepository<ArduinoLogsModel, TimeTrackerEntity>, ITimeTrackerRepository
//{
//    public TimeTrackerRepository(TimeTrackerContext context) : base(context)
//    {
//    }

//    private DbSet<TimeTrackerEntity> GetUserDbSet()
//    {
//        if (Context.TimeTrackers is null)
//            throw new InvalidOperationException("Database configuration not setup correctly");
//        return Context.TimeTrackers;
//    }

//    protected override ArduinoLogsModel Map(TimeTrackerEntity entity)
//    {
//        return TimeTrackerEntityMapper.Map(entity);
//    }

//    protected override TimeTrackerEntity Map(ArduinoLogsModel model)
//    {
//        return TimeTrackerEntityMapper.Map(model);
//    }
//}
