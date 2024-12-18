using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;

public class GetTimeTrackerEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<TimeTrackerResponse>
{
    public GetTimeTrackerEndpoint()
    {
    }

    [HttpGet("api/timetrackers/{timetrackerId:guid}")]
    [ProducesResponseType(typeof(TimeTrackerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get TimeTracker by TimeTracker id",
        Description = "Get TimeTracker by TimeTracker id",
        OperationId = "GetTimeTracker",
        Tags = new[] { Constants.ApiTags.TimeTracker })
    ]
    public override async Task<ActionResult<TimeTrackerResponse>> HandleAsync([FromRoute] Guid timetrackerId, CancellationToken cancellationToken = default)
    {
        return new ActionResult<TimeTrackerResponse>(new TimeTrackerResponse(timetrackerId));
    }
}
