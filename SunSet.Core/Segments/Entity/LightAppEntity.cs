using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
