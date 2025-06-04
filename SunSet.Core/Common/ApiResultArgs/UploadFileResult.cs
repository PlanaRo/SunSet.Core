using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class UploadFileResult
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    public override string ToString() =>
        $"[{nameof(UploadFileResult)}] FileId: {FileId}";
}
