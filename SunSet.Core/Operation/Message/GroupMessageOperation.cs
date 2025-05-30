using System.Text.Json;
using System.Text.Json.Nodes;
using SunSet.Core.Milky.Message;

namespace SunSet.Core.Operation.Message;

[MikyEventType("message_receive")]
internal class GroupMessageOperation : IOperationHandler
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if(node.Deserialize<MilkyGroupMessage>() is { } msg)
        {
            await bot.Invoke.Call(bot, msg, token);
        }
    }
}
