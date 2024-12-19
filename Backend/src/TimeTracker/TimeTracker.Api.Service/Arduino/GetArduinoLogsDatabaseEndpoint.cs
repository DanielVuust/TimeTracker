using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;
using TimeRegistration.TimeTracker.Domain.TimeTracker;

namespace TimeRegistration.TimeTracker.Api.Service.Arduino;

public class GetArduinoLogsDatabaseEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<IEnumerable<ArduinoLogsModel?>>
{
    private readonly ITimeTrackerRepository _timeTrackerRepository;

    public GetArduinoLogsDatabaseEndpoint(ITimeTrackerRepository timeTrackerRepository)
    {
        _timeTrackerRepository = timeTrackerRepository;
    }

    [HttpGet("api/arduino/{arduinoId:guid}/database/logs")]
    [ProducesResponseType(typeof(ArduinoLogsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get Arduino log data by Arduino id",
        Description = "Get Arduino log data by Arduino id",
        OperationId = "GetArduinoLogs",
        Tags = new[] { Constants.ApiTags.Arduino })
    ]
    public override async Task<ActionResult<IEnumerable<ArduinoLogsModel?>>> HandleAsync([FromRoute] Guid arduinoId, CancellationToken cancellationToken = default)
    {
        var arduinoLogsResponse = await _timeTrackerRepository.GetArduinoLogsByArduinoId(arduinoId);
        return new ActionResult<IEnumerable<ArduinoLogsModel?>>(arduinoLogsResponse);
    }
}
