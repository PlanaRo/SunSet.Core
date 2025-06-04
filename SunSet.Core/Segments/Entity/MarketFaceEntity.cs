using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("market_face")]
public class MarketFaceEntity : BaseEntity
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    public override string ToString() =>
        $"[MarketFace: {Url}]";
}
