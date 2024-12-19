using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace TimeRegistration.TimeTracker.Api.Service.Arduino;

public class ArduinoOperationRequest<T> 
{
    [FromRoute(Name = "arduinoId")] public Guid ArduinoId { get; set; }

    [FromBody] 
    public T Details { get; set; }
}