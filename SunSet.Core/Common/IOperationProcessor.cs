using System.Text.Json.Nodes;

namespace SunSet.Core.Common;

/// <summary>
/// Defines a contract for processing operations asynchronously within a bot context.
/// </summary>
/// <remarks>Implementations of this interface are responsible for handling operations represented as JSON nodes
/// within the context of a bot. The operation processing is performed asynchronously and can be canceled using a
/// cancellation token.</remarks>
internal interface IOperationProcessor
{
    Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token);
}
