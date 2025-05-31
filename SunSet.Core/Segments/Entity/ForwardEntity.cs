using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Segments.Entity;

[EntityType("forward")]
public class ForwardEntity : BaseEntity
{
    [JsonPropertyName("forward_id")]
    public string ForwardId { get; set; } = string.Empty;

    public override string ToString() => $"[Forward: {ForwardId}]";
}
