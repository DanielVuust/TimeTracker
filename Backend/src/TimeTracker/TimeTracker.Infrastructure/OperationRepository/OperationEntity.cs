using TimeRegistration.TimeTracker.Domain.Operations;
using TimeRegistration.TimeTracker.Infrastructure.BaseRepository;

namespace TimeRegistration.TimeTracker.Infrastructure.OperationRepository;
public class OperationEntity : BaseEntity
{
    public string RequestId { get; }
    public Guid ArduinoId { get; }
    public string CreatedBy { get; }
    public OperationName OperationName { get; }
    public DateTime? CompletedUtc { get; }
    public OperationStatus Status { get; }
    public string? Data { get; }

    public OperationEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, string requestId, Guid arduinoId,
       string createdBy, OperationName operationName, OperationStatus status, DateTime? completedUtc, string? data) :
       base(id, createdUtc, modifiedUtc)
    {
        ModifiedUtc = modifiedUtc;
        RequestId = requestId;
        ArduinoId = arduinoId;
        CreatedBy = createdBy;
        OperationName = operationName;
        Status = status;
        CompletedUtc = completedUtc;
        Data = data;
    }
}
