using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SunSet.Core.Entity;
using SunSet.Core.Milky.Info;

namespace SunSet.Core.Milky.Message;

public class MilkyGroupMessage : MilkyBaseData
{
    [JsonPropertyName("message_scene")]
    public string MessageScene { get; set; } = string.Empty;

    [JsonPropertyName("peer_id")]
    public uint GroupUin { get; set; }

    [JsonPropertyName("sender_id")]
    public uint SenderUin { get; set; }

    [JsonPropertyName("time")]
    public uint Time { get; set; }

    [JsonPropertyName("segments")]
    public List<MilkySegment> Segments { get; set; } = [];

    [JsonPropertyName("group")]
    public Group Group { get; set; } = new();

    [JsonPropertyName("group_member")]
    public GroupMember Sender { get; set; } = new();
}
