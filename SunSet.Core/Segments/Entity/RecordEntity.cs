using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("record")]
public class RecordEntity : BaseEntity
{
    [JsonPropertyName("resource_id")]
    public string ResourceId { get; set; } = string.Empty;

    [JsonPropertyName("Temp_url")]
    public string TempUrl { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    public override string ToString() =>
        $"[Record: {ResourceId}, TempUrl: {TempUrl}, Duration: {Duration}]";
}
