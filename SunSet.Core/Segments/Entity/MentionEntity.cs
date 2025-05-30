using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Segments.Entity;

[EntityType("mention")]
public class MentionEntity : BaseEntity
{
    [JsonPropertyName("user_id")]
    public uint UserUin { get; set; }
}
