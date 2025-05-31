using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Common.ApiResultArgs;

public class TempResourceResult
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty; 

    public override string ToString() => $"[{nameof(TempResourceResult)}: Url={Url}]";
}
