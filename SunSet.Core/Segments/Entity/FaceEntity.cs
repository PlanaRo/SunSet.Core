using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("face")]
public class FaceEntity : BaseEntity
{
    [JsonPropertyName("face_id")]
    public string FaceId { get; init; } = string.Empty;

    public override string ToString() => $"[Face: {FaceId}]";
}
