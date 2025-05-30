using System.Text.Json.Serialization;

namespace SunSet.Core.Milky.Info;

public class GroupMember
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; init; } = string.Empty;

    [JsonPropertyName("card")]
    public string Card { get; init; } = string.Empty;

    [JsonPropertyName("tile")]
    public string Tile { get; init; } = string.Empty;

    [JsonPropertyName("sex")]
    public string Sex { get; init; } = string.Empty;

    [JsonPropertyName("level")]
    public uint Level { get; init; }

    [JsonPropertyName("role")]
    public string Role { get; init; } = string.Empty;

    [JsonPropertyName("join_time")]
    public uint JoinTime { get; init; }

    [JsonPropertyName("last_sent_time")]
    public uint LastSentTime { get; init; }
}
