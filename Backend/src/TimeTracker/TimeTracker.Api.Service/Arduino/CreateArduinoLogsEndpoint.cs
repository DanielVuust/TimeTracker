using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.Api.Service.Arduino;

public class CreateArduinoLogsEndpoint : EndpointBaseAsync.WithRequest<CreateArduinoLogsRequestWithBody>.WithActionResult
{
    private readonly ITimeTrackerCsvComponent _timeTrackerCsvComponent;

    public CreateArduinoLogsEndpoint(ITimeTrackerCsvComponent timeTrackerCsvComponent)
    {
        _timeTrackerCsvComponent = timeTrackerCsvComponent;
    }

    [HttpPost("api/arduino/{arduinoId:guid}/log")]
    [ProducesResponseType(typeof(ArduinoLogsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Create Arduino log data by Arduino id",
        Description = "Create Arduino log data by Arduino id",
        OperationId = "CreateArduinoLogs",
        Tags = new[] { Constants.ApiTags.Arduino })
    ]
    public override async Task<ActionResult> HandleAsync([FromRoute] CreateArduinoLogsRequestWithBody request, CancellationToken cancellationToken = default)
    {
        _timeTrackerCsvComponent.UpsertArduinoLogsToCsv(request.Details, request.ArduinoId.ToString());

        return new EmptyResult();
    }
}


public sealed class CreateArduinoLogsRequestWithBody : ArduinoOperationRequest<IEnumerable<ArduinoLog>>
{
}