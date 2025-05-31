using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_invited_join_request")]
internal class GroupInvitedJoinRequestOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupInvitedJoinRequest>() is { } request)
        {
            await bot.Invoke.Call(bot, request, token);
        }
    }
}


public class MilkyGroupInvitedJoinRequest : MilkyBaseData
{
    /// <summary>
    /// 邀请请求ID
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = string.Empty;
    /// <summary>
    /// QQ
    /// </summary>
    [JsonPropertyName("operator_id")]
    public uint OperatorId { get; init; }
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }
    /// <summary>
    /// 邀请者id
    /// </summary>
    [JsonPropertyName("invitee_id")]
    public uint InviteeUin { get; init; }
}