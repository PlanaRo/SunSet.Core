using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("image")]
public class ImageEntity : BaseEntity
{
    [JsonPropertyName("resource_id")]
    public string ResourceId { get; set; } = string.Empty;

    [JsonPropertyName("Temp_url")]
    public string TempUrl { get; set; } = string.Empty;

    [JsonPropertyName("summary")]
    public string Summary { get; set; } = "图片";

    [JsonPropertyName("sub_type")]
    public string SubType { get; set; } = "normal";
}
