using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;

namespace TimeRegistration.TimeTracker.Api.Service.Arduino;

public class CreateArduinoLogsEndpoint : EndpointBaseAsync.WithRequest<CreateArduinoLogsRequestWithBody>.WithActionResult<IEnumerable<ArduinoLogsResponse>>
{
    public CreateArduinoLogsEndpoint()
    {
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
    public override async Task<ActionResult<IEnumerable<ArduinoLogsResponse>>> HandleAsync([FromRoute] CreateArduinoLogsRequestWithBody request, CancellationToken cancellationToken = default)
    {
        //Call service/component to create Arduino logs
        var arduinoLogsResponses = new List<ArduinoLogsResponse>() 
        {
            new ArduinoLogsResponse(request.Details.Timestamp, request.Details.Status)
        };

        return new ActionResult<IEnumerable<ArduinoLogsResponse>>(arduinoLogsResponses);
    }
}


public sealed class CreateArduinoLogsRequestWithBody : ArduinoOperationRequest<CreateUserRequestDetails>
{
}

[SwaggerSchema(Nullable = false, Required = new[] { "timestamp", "status" })]
public sealed class CreateUserRequestDetails
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}
