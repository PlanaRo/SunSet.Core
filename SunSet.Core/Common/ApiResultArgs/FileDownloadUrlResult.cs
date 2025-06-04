using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class FileDownloadUrlResult
{
    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; set; } = string.Empty;

    public override string ToString() =>
        $"[{nameof(FileDownloadUrlResult)}: DownloadUrl={DownloadUrl}]";
}
