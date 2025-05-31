using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Segments.Entity;

[EntityType("video")]
public class VideoEntity : BaseEntity
{
    [JsonPropertyName("resource_id")]
    public string ResourceId { get; set; } = string.Empty;

    [JsonPropertyName("Temp_url")]
    public string TempUrl { get; set; } = string.Empty;

    public override string ToString() =>
        $"[Video: {ResourceId}, TempUrl: {TempUrl}]";   
}
