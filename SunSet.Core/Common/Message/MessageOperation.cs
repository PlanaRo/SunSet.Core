using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Common;
using SunSet.Core.Milky;
using SunSet.Core.Milky.Info;
using SunSet.Core.Segments;

namespace SunSet.Core.Operation.Message;

[CustomEvent("message_receive")]
internal class MessageOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyBaseMessage>() is { } msg)
        {
            switch (msg.MessageScene)
            {
                case "group":
                    await bot.Invoke.Call(bot, node.Deserialize<MilkyGroupMessage>()!, token);
                    break;
                case "friend":
                    await bot.Invoke.Call(bot, node.Deserialize<MilkyFriendMessage>()!, token);
                    break;
                case "temp":
                    await bot.Invoke.Call(bot, node.Deserialize<MilkyTempMessage>()!, token);
                    break;
                default:
                    throw new NotSupportedException($"Message scene '{msg.MessageScene}' is not supported.");
            }
        }
    }
}

public class MilkyBaseMessage : MilkyBaseData
{
    [JsonPropertyName("message_scene")]
    public string MessageScene { get; init; } = string.Empty;

    [JsonPropertyName("peer_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("sender_id")]
    public uint SenderUin { get; init; }

    [JsonPropertyName("time")]
    public uint Time { get; init; }

    [JsonPropertyName("segments")]
    public MessageChain Segments { get; init; } = [];
}

public class MilkyFriendMessage : MilkyBaseMessage
{
    [JsonPropertyName("friend")]
    public Friend Friend { get; set; } = new();

    [JsonPropertyName("client_seq")]
    public long ClientSeq { get; init; } = 0;
}

public class MilkyGroupMessage : MilkyBaseMessage
{
    [JsonPropertyName("group")]
    public Group Group { get; set; } = new();

    [JsonPropertyName("group_member")]
    public GroupMember Sender { get; set; } = new();
}

public class MilkyTempMessage : MilkyBaseMessage
{
    [JsonPropertyName("group")]
    public Group Group { get; set; } = new();
}