using TimeRegistration.TimeTracker.ApplicationServices.Repositories.Operations;
using TimeRegistration.TimeTracker.Domain.Operations;
using TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository;
using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace TimeRegistration.TimeTracker.Infrastructure.OperationRepository;
public class OperationRepository : BaseRepository<Operation, OperationEntity>, IOperationRepository
{
    public OperationRepository(TimeTrackerContext context) : base(context)
    {
    }

    public async Task<Operation?> GetByRequestId(string requestId)
    {
        var operationEntity = await GetOperationsDbSet()
            .Where(x => x.RequestId == requestId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
        return operationEntity is null ? null : Map(operationEntity);
    }

    public async Task<ICollection<Operation>> GetTimeTrackerOperations(Guid timetrackerId)
    {
        var timetracker = await GetOperationsDbSet()
                    .Where(x => x.TimeTrackerId == timetrackerId)
                    .AsNoTracking()
                    .ToListAsync();
        return timetracker.Select(Map).ToImmutableHashSet();
    }

    private DbSet<OperationEntity> GetOperationsDbSet()
    {
        if (Context.Operations is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.Operations;
    }

    protected override Operation Map(OperationEntity entity)
    {
        return OperationMapper.Map(entity);
    }

    protected override OperationEntity Map(Operation model)
    {
        return OperationMapper.Map(model);
    }

  
}
