using SunSet.Core.Entity;
using SunSet.Core.Segments.Entity;
using System.Reflection;

namespace SunSet.Core.Segments;

public class MessageChain : List<MilkySegment>
{
    public uint GroupUin { get; set; }

    public uint UserUin { get; init; }

    public MessageChain() { }

    public static MessageChain Group(uint groupUin)
    {
        return new MessageChain
        {
            GroupUin = groupUin
        };
    }

    public static MessageChain Friend(uint userUin)
    {
        return new MessageChain
        {
            UserUin = userUin
        };
    }

    public MessageChain Text(string text)
    {
        Entity(new TextEntity { Text = text });
        return this;
    }

    public MessageChain MentionAll()
    {
        Entity(new MentionAllEntity());
        return this;
    }

    public MessageChain Mention(uint userUin)
    {
        Entity(new MentionEntity { UserUin = userUin });
        return this;
    }

    public MessageChain Face(string faceId)
    {
        Entity(new FaceEntity { FaceId = faceId });
        return this;
    }

    public MessageChain LightApp(string appName, string payload)
    {
        Entity(new LightAppEntity
        {
            AppName = appName,
            JsonPayload = payload
        });
        return this;
    }

    public MessageChain Record(string resourceId, string tempUrl)
    {
        Entity(new RecordEntity
        {
            ResourceId = resourceId,
            TempUrl = tempUrl,
        });
        return this;
    }

    public MessageChain Reply(long replyId = 0)
    {
        Entity(new ReplyEntity
        {
            MessageSeq = replyId,
        });
        return this;
    }

    public MessageChain MarketFace(string url)
    {
        Entity(new MarketFaceEntity
        {
            Url = url
        });
        return this;
    }

    public MessageChain Video(string resourceId, string tempUrl)
    {
        Entity(new VideoEntity
        {
            ResourceId = resourceId,
            TempUrl = tempUrl,
        });
        return this;
    }

    public MessageChain Forward(string id)
    {
        Entity(new ForwardEntity
        {
            ForwardId = id
        });
        return this;
    }

    public MessageChain Xml(string serviceId, string payload)
    {
        Entity(new XmlEntity { ServiceId = serviceId, XmlPayload = payload });
        return this;
    }

    public MessageChain Image(string resourceId, string tempUrl, string summary = "图片", string subType = "normal")
    {
        Entity(new ImageEntity
        {
            ResourceId = resourceId,
            TempUrl = tempUrl,
            Summary = summary,
            SubType = subType
        });
        return this;
    }

    public MessageChain Entity(BaseEntity entity)
    {
        Add(new()
        {
            Type = entity.GetType().GetCustomAttribute<EntityTypeAttribute>()?.Type ?? "Text",
            Entity = entity
        });
        return this;
    }

    public List<T> GetEntities<T>() where T : BaseEntity
    {
        return [.. this.Where(segment => segment.Entity is T).Select(segment => (T)segment.Entity)];
    }

    public override string ToString() =>
        string.Join(" ", this.Select(segment => segment.ToString()));
}
