using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class CreateFolderResult
{
    [JsonPropertyName("folder_id")]
    public string FolderId { get; set; } = string.Empty;

    public override string ToString() =>
        $"[{nameof(CreateFolderResult)}] FolderId: {FolderId}";
}
