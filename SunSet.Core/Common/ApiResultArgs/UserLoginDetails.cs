using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class UserLoginDetails
{
    [JsonPropertyName("nickname")]
    public string Nickname { get; init; } = string.Empty;

    [JsonPropertyName("uin")]
    public uint Uin { get; init; }

    public override string ToString() =>
        $"[{nameof(UserLoginDetails)}] Nickname: {Nickname}, Uin: {Uin}";
}
