using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_file_upload")]
internal class GroupFileUploadOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupFileUpload>() is { } upload)
        {
            await bot.Invoke.Call(bot, upload);
        }
    }
}

public class MilkyGroupFileUpload : MilkyBaseData
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }
    /// <summary>
    /// 上传者QQ
    /// </summary>
    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    /// <summary>
    /// 文件名
    /// </summary>
    [JsonPropertyName("file_name")]
    public string FileName { get; init; } = string.Empty;
    /// <summary>
    /// 文件大小
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; init; }

    public override string ToPreviewString()
    {
        return $"[{nameof(MilkyGroupFileUpload)}] GroupId: {GroupUin}, UserId: {UserUin}, FileName: {FileName}, FieSize: {FileSize} bytes";
    }
}