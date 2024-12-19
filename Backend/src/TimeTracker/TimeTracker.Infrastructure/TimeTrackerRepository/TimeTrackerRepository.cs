using TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;
using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
public class TimeTrackerRepository : BaseRepository<ArduinoLogsModel, TimeTrackerEntity>, ITimeTrackerRepository
{
    private readonly TimeTrackerContext _context;

    public TimeTrackerRepository(TimeTrackerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ArduinoLogsModel?>> GetArduinoLogsByArduinoId(Guid arduinoId)
    {
        var entities = await GetTimeTrackerEntityById(arduinoId);

        if (entities.IsNullOrEmpty())
        {
            return null;
        }

        var models = new List<ArduinoLogsModel>();

        foreach (var entity in entities)
        {
            models.Add(Map(entity));
        }

        return models;
    }

    private DbSet<TimeTrackerEntity> GetUserDbSet()
    {
        if (Context.TimeTrackers is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.TimeTrackers;
    }

    protected override ArduinoLogsModel Map(TimeTrackerEntity entity)
    {
        return TimeTrackerEntityMapper.Map(entity);
    }

    protected override TimeTrackerEntity Map(ArduinoLogsModel model)
    {
        return TimeTrackerEntityMapper.Map(model);
    }

    private async Task<IEnumerable<TimeTrackerEntity>?> GetTimeTrackerEntityById(Guid arduinoId)
    {
        return await _context.TimeTrackers.Where(p => p.ArduinoId == arduinoId).ToListAsync();
    }
}
