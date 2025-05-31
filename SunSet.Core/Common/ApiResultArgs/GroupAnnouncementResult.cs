using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Common.ApiResultArgs;

public class GroupAnnouncementResult
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("announcement_id")]
    public string AnnouncementId { get; init; } = string.Empty;

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("time")]
    public uint Time { get; init; }

    [JsonPropertyName("content")]
    public string Content { get; init; } = string.Empty;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; init; } = string.Empty;
}
