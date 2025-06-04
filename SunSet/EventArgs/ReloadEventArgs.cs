using SunSet.Core.Segments;

namespace SunSet.EventArgs;

public class ReloadEventArgs(uint group)
{
    public MessageChain Message { get; } = MessageChain.Group(group);
}
