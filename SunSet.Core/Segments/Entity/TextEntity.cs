using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("text")]
public class TextEntity : BaseEntity
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    public override string ToString() =>
        $"[Text: {Text}]";
}
