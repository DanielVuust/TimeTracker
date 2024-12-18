using TimeRegistration.TimeTracker.Domain.Operations;

namespace TimeRegistration.TimeTracker.ApplicationServices.Repositories.Operations;

public interface IOperationService
{
    Task<Operation> QueueOperation(Operation operation);
    Task<Operation?> GetOperationByRequestId(string requestId);
    Task<Operation?> UpdateOperationStatus(string requestId, OperationStatus operationStatus);
    Task<ICollection<Operation>> GetTimeTrackerOperations(Guid TimeTrackerId);
}