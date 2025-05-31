using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class PrivateMessageResult
{
    [JsonPropertyName("message_seq")]
    public long MessageSeq { get; init; }

    [JsonPropertyName("time")]
    public long Time { get; init; }

    [JsonPropertyName("client_seq")]
    public long ClientSeq { get; init; }
}
