using System.Text.Json;
using System.Text.Json.Nodes;
using SunSet.Core.Milky.Message;

namespace SunSet.Core.Operation.Message;

[MikyEventType("message_receive")]
internal class GroupMessageOperation : IOperationHandler
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node)
    {
        if(node.Deserialize<MilkyGroupMessage>() is { } msg)
        {
            bot.Invoke.Call(msg);
        }
    }
}
