using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class TimeTrackerResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public TimeTrackerResponse(Guid id)
    {
        Id = id;
    }
}


