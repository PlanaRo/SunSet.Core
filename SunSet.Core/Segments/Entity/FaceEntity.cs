using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Segments.Entity;

[EntityType("face")]
public class FaceEntity : BaseEntity
{
    [JsonPropertyName("face_id")]
    public string FaceId { get; init; } = string.Empty;

    public override string ToString() => $"[Face: {FaceId}]";
}
