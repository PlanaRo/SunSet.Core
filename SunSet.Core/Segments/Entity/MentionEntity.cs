using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("mention")]
public class MentionEntity : BaseEntity
{
    [JsonPropertyName("user_id")]
    public uint UserUin { get; set; }
}
