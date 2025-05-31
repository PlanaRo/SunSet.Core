using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SunSet.Core.Milky.Info;

namespace SunSet.Core.Common.ApiResultArgs;

public class GetGroupFilesResult
{
    [JsonPropertyName("files")]
    public List<GroupFile> Files { get; init; } = [];

    [JsonPropertyName("folders")]
    public List<GroupFolder> Folders { get; init; } = [];
}
