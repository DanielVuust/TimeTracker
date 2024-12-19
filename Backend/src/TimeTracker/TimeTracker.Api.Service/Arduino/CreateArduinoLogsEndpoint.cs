using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.Api.Service.Arduino;

public class CreateArduinoLogsEndpoint : EndpointBaseAsync.WithRequest<CreateArduinoLogsRequestWithBody>.WithActionResult<IEnumerable<ArduinoLogsResponse>>
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
    public override async Task<ActionResult<IEnumerable<ArduinoLogsResponse>>> HandleAsync([FromRoute] CreateArduinoLogsRequestWithBody request, CancellationToken cancellationToken = default)
    {
        var arduinoLogsModel = new List<ArduinoLogsModel>();

        foreach (var detail in request.Details)
        {
            arduinoLogsModel.Add(ArduinoLogsModel.Create(request.ArduinoId, detail.Timestamp, detail.Status));
        }

        var arduinoLogsModelResponse = _timeTrackerCsvComponent.UpsertArduinoLogsToCsv(arduinoLogsModel);

        var arduinoLogsResponse = arduinoLogsModelResponse.Select(x => new ArduinoLogsResponse(x.LogsModel.Timestamp, x.LogsModel.Status));

        return new ActionResult<IEnumerable<ArduinoLogsResponse>>(arduinoLogsResponse);
    }
}


public sealed class CreateArduinoLogsRequestWithBody : ArduinoOperationRequest<IEnumerable<CreateUserRequestDetails>>
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
