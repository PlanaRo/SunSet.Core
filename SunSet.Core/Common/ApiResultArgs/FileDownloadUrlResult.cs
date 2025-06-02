using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Common.ApiResultArgs;

public class FileDownloadUrlResult
{
    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; set; } = string.Empty;

    public override string ToString() =>
        $"[{nameof(FileDownloadUrlResult)}: DownloadUrl={DownloadUrl}]";
}
