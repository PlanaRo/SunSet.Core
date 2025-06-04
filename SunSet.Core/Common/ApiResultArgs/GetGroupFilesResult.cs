using SunSet.Core.Milky.Info;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class GetGroupFilesResult
{
    [JsonPropertyName("files")]
    public List<GroupFile> Files { get; init; } = [];

    [JsonPropertyName("folders")]
    public List<GroupFolder> Folders { get; init; } = [];

    public override string ToString() =>
        $"[{nameof(GetGroupFilesResult)} Files: {Files.Count}, Folders: {Folders.Count}]";
}
