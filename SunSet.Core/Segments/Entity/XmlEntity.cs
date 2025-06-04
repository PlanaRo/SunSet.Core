using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("xml")]
public class XmlEntity : BaseEntity
{
    [JsonPropertyName("service_id")]
    public string ServiceId { get; set; } = string.Empty;

    [JsonPropertyName("xml_payload")]
    public string XmlPayload { get; set; } = string.Empty;

    public override string ToString() =>
        $"[Xml: ServiceId: {ServiceId}, XmlPayload: {XmlPayload}]";
}
