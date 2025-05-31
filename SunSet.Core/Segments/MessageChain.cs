using SunSet.Core.Entity;
using SunSet.Core.Segments.Entity;

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
        Add(new()
        {
            Type = "text",
            Entity = new TextEntity
            {
                Text = text
            }
        });
        return this;
    }

    public MessageChain Image(string resourceId, string tempUrl, string summary = "图片", string subType = "normal")
    {
        Add(new()
        {
            Type = "image",
            Entity = new ImageEntity
            {
                ResourceId = resourceId,
                TempUrl = tempUrl,
                Summary = summary,
                SubType = subType
            }
        });
        return this;
    }

    public override string ToString() =>
        string.Join(" ", this.Select(segment => segment.ToString()));
}
