using Microsoft.AspNetCore.Mvc;

namespace TimeRegistration.TimeTracker.Api.Service.Models;

public class TimeTrackerOperationRequest<T> : OperationRequest
{
    [FromBody] public T Details { get; set; }
}
