using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeRegistration.TimeTracker.ApplicationServices.Component;

namespace TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;

public class GetArduinoLogsEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<IEnumerable<ArduinoLog>>
{
    private readonly ITimeTrackerCsvComponent _timeTrackerCsvComponent;

    public GetArduinoLogsEndpoint(ITimeTrackerCsvComponent timeTrackerCsvComponent)
    {
        _timeTrackerCsvComponent = timeTrackerCsvComponent;
    }

    [HttpGet("api/arduino/{arduinoId:guid}/logs")]
    [ProducesResponseType(typeof(ArduinoLogsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get Arduino log data by Arduino id",
        Description = "Get Arduino log data by Arduino id",
        OperationId = "GetArduinoLogs",
        Tags = new[] { Constants.ApiTags.Arduino })
    ]
    public override async Task<ActionResult<IEnumerable<ArduinoLog>>> HandleAsync([FromRoute] Guid arduinoId, CancellationToken cancellationToken = default)
    {
        var arduinoLogsResponse = this._timeTrackerCsvComponent.GetArduinoLogsFromCsv(arduinoId.ToString());
        return new ActionResult<IEnumerable<ArduinoLog>>(arduinoLogsResponse);
    }
}