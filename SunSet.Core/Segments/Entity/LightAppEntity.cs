using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("light_app")]
public class LightAppEntity : BaseEntity
{
    [JsonPropertyName("app_name")]
    public string AppName { get; set; } = string.Empty;

    [JsonPropertyName("json_payload")]
    public string JsonPayload { get; set; } = string.Empty;

    public override string ToString() =>
        $"[LightApp: {AppName}, Payload: {JsonPayload}]";
}
