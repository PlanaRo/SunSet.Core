using System.Text.Json.Nodes;

namespace SunSet.Core.Common;

internal interface IOperationProcessor
{
    Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token);

}
