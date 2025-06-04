using System.Text.Json.Serialization;

namespace SunSet.Core.Milky.Info;

public class GroupFile
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_name")]
    public string FileName { get; init; } = string.Empty;

    [JsonPropertyName("file_size")]
    public long FileSize { get; init; } = 0;

    [JsonPropertyName("parent_folder_id")]
    public string ParentFolderId { get; init; } = string.Empty;

    [JsonPropertyName("uploaded_time")]
    public long UploadedTime { get; init; }

    [JsonPropertyName("expire_time")]
    public long ExpireTime { get; init; }

    [JsonPropertyName("uploader_id")]
    public uint UploaderUin { get; init; }

    [JsonPropertyName("downloaded_times")]
    public int DownloadedTimes { get; init; }
}
