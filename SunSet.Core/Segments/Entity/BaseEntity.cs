using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[JsonDerivedType(typeof(TextEntity))]
[JsonDerivedType(typeof(MentionAllEntity))]
[JsonDerivedType(typeof(MentionEntity))]
[JsonDerivedType(typeof(ImageEntity))]
[JsonDerivedType(typeof(FaceEntity))]
[JsonDerivedType(typeof(LightAppEntity))]
[JsonDerivedType(typeof(MarketFaceEntity))]
[JsonDerivedType(typeof(ForwardEntity))]
[JsonDerivedType(typeof(RecordEntity))]
[JsonDerivedType(typeof(ReplyEntity))]
[JsonDerivedType(typeof(VideoEntity))]
[JsonDerivedType(typeof(XmlEntity))]
public class BaseEntity
{
}
