using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class TempResourceResult
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    public override string ToString() => $"[{nameof(TempResourceResult)}: Url={Url}]";
}
