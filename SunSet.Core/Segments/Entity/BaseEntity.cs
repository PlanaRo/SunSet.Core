using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[JsonDerivedType(typeof(TextEntity))]
[JsonDerivedType(typeof(MentionAllEntity))]
[JsonDerivedType(typeof(MentionEntity))]
[JsonDerivedType(typeof(ImageEntity))]
public class BaseEntity
{
}
