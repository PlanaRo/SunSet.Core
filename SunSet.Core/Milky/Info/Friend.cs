
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace SunSet.Core.Milky.Info;

public class Friend
{
    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; init; } = string.Empty;

    [JsonPropertyName("remark")]
    public string Remark { get; init; } = string.Empty;

    [JsonPropertyName("qid")]
    public string Qid { get; init; } = string.Empty;

    [JsonPropertyName("sex")]
    public string Sex { get; init; } = string.Empty;

    [JsonPropertyName("category")]
    public FriendCategory Category { get; init; } = new();
}
