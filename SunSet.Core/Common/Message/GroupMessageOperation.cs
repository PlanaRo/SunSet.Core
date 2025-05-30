using System.Text.Json;
using System.Text.Json.Nodes;
using SunSet.Core.Common;
using SunSet.Core.Milky.Message;

namespace SunSet.Core.Operation.Message;

[CustomEvent("message_receive")]
internal class GroupMessageOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if(node.Deserialize<MilkyGroupMessage>() is { } msg)
        {
            await bot.Invoke.Call(bot, msg, token);
        }
    }
}
