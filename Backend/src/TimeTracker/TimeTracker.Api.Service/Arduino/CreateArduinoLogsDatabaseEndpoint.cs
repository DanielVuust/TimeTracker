using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.ApplicationServices.Repositories.TimeTracker;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.Api.Service.Arduino;

public class CreateArduinoLogsDatabaseEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult
{
    private readonly ITimeTrackerCsvComponent _timeTrackerCsvComponent;
    private readonly ITimeTrackerRepository _timeTrackerRepository;

    public CreateArduinoLogsDatabaseEndpoint(ITimeTrackerCsvComponent timeTrackerCsvComponent, ITimeTrackerRepository timeTrackerRepository)
    {
        _timeTrackerCsvComponent = timeTrackerCsvComponent;
        _timeTrackerRepository = timeTrackerRepository;
    }

    [HttpPost("api/arduino/{arduinoId:guid}/logs")]
    [ProducesResponseType(typeof(ArduinoLogsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Create Arduino log data in database by Arduino id",
        Description = "Create Arduino log data in database by Arduino id",
        OperationId = "CreateArduinoLogsDataBase",
        Tags = new[] { Constants.ApiTags.Arduino })
    ]
    public override async Task<ActionResult> HandleAsync([FromRoute] Guid arduinoId, CancellationToken cancellationToken = default)
    {
        var arduinoLogsFromCsv = _timeTrackerCsvComponent.GetArduinoLogsFromCsv(arduinoId.ToString());


        foreach (var log in arduinoLogsFromCsv)
        {
            var arduinoLogsModel = ArduinoLogsModel.Create(arduinoId, log.Timestamp, log.Status);
            await _timeTrackerRepository.Upsert(arduinoLogsModel);
        }
        return new EmptyResult();
    }
}
