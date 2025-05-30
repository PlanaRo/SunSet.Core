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
}
