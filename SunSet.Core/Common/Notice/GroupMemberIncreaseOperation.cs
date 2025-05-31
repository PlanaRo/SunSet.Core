using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_member_increase")]
internal class GroupMemberIncreaseOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupMemberIncrease>() is { } increase)
        {
            await bot.Invoke.Call(bot, increase, token);
        }
    }
}

public class MilkyGroupMemberIncrease : MilkyBaseData
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }
    /// <summary>
    /// 新成员QQ
    /// </summary>
    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }
    /// <summary>
    /// 操作人QQ
    /// </summary>
    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }

    [JsonPropertyName("invitor_id")]
    public uint InvitorUin { get; init; }
}