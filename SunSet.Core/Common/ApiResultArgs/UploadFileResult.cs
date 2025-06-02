using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Common.ApiResultArgs;

public class UploadFileResult
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    public override string ToString() =>
        $"[{nameof(UploadFileResult)}] FileId: {FileId}";
}
