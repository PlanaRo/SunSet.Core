using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Milky.Info;

public class GroupFolder
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("folder_id")]
    public string FolderId { get; init; }

    [JsonPropertyName("folder_name")]
    public string FolderName { get; init; } = string.Empty;

    [JsonPropertyName("parent_folder_id")]
    public string ParentFolderId { get; init; } = string.Empty;

    [JsonPropertyName("created_time")]
    public long CreatedTime { get; init; }

    [JsonPropertyName("last_modified_time")]
    public long LastModifiedTime { get; init; }

    [JsonPropertyName("creator_id")]
    public int CreatorId { get; init; }


    [JsonPropertyName("file_count")]
    public int FileCount { get; init; }
}
