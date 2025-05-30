using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Milky;

public class MilkyEventArgs
{
    [JsonPropertyName("time")]
    public long Time { get; set; }

    [JsonPropertyName("self_id")]
    public long SelfUin { get; set; }

    [JsonPropertyName("event_type")]
    public string EventType { get; set; } = string.Empty;

#nullable disable
    [JsonPropertyName("data")]
    public JsonObject Payload { get; set; }
#nullable enable
}
