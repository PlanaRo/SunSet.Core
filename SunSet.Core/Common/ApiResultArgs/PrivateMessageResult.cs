using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
