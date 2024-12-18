using TimeRegistration.TimeTracker.Domain.Operations;

namespace TimeRegistration.TimeTracker.ApplicationServices.Repositories.Operations;
public interface IOperationRepository : IBaseRepository<Operation>
{
    Task<Operation?> GetByRequestId(string requestId);
    Task<ICollection<Operation>> GetTimeTrackerOperations(Guid timetrackerId);
}