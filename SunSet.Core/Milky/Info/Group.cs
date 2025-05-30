using System.Text.Json.Serialization;

namespace SunSet.Core.Milky.Info;

public class Group
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("member_count")]
    public uint MemberCount { get; init; }

    [JsonPropertyName("max_member_count")]
    public uint MaxMemberCount { get; init; }
}
