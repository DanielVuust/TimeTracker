using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;

public class GetArduinoLogsEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<ArduinoLogsResponse>
{
    public GetArduinoLogsEndpoint()
    {
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
    public override async Task<ActionResult<ArduinoLogsResponse>> HandleAsync([FromRoute] Guid arduinoId, CancellationToken cancellationToken = default)
    {
        //var uniqueId = Guid.NewGuid();  
        var timeStamp = DateTime.UtcNow;
        var status = "Start";
        //Call service/component to get Arduino logs from database

        return new ActionResult<ArduinoLogsResponse>(new ArduinoLogsResponse(timeStamp, status));
    }
}