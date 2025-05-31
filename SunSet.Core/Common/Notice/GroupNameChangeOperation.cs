using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_name_change")]
internal class GroupNameChangeOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupNameChange>() is { } change)
        {
            await bot.Invoke.Call(bot, change, token);
        }
    }
}

public class MilkyGroupNameChange : MilkyBaseData
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }
    /// <summary>
    /// 新群名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }
}