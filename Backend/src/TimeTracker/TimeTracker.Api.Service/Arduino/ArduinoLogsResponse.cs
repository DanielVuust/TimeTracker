using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace TimeRegistration.TimeTracker.Api.Service.GetTimeTracker;

[SwaggerSchema(Nullable = false, Required = new[] { "arduinoId", "timestamp", "status" })]
public class ArduinoLogsResponse
{
    public ArduinoLogsResponse(DateTime timestamp, string status)
    {
        Timestamp = timestamp;
        Status = status;
    }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}


