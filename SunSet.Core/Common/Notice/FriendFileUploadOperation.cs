using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("friend_file_upload")]
internal class FriendFileUploadOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyFriendFileUpload>() is { } upload)
        {
            await bot.Invoke.Call(bot, upload, token);
        }
    }
}

public class MilkyFriendFileUpload : MilkyBaseData
{
    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_name")]
    public string FileName { get; init; } = string.Empty;

    [JsonPropertyName("file_size")]
    public long FileSize { get; init; }

    [JsonPropertyName("is_self")]
    public bool IsSelf { get; init; }
}