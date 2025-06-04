using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("forward")]
public class ForwardEntity : BaseEntity
{
    [JsonPropertyName("forward_id")]
    public string ForwardId { get; set; } = string.Empty;

    public override string ToString() => $"[Forward: {ForwardId}]";
}
