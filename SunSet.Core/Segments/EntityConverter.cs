using SunSet.Core.Segments;
using SunSet.Core.Segments.Entity;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Entity;

internal class EntityConverter : JsonConverter<MilkySegment>
{
    public readonly Dictionary<string, Type> _entityTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsSubclassOf(typeof(BaseEntity)) && t.IsDefined(typeof(EntityTypeAttribute)) && !t.IsAbstract)
        .ToDictionary(t => t.GetCustomAttribute<EntityTypeAttribute>()!.Type, t => t);


    public override MilkySegment? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonNode = JsonSerializer.Deserialize<JsonNode>(ref reader, options);
        if (jsonNode?["type"]?.ToString() is string type)
        {
            if (_entityTypes.TryGetValue(type, out var entityType))
            {
                var data = (BaseEntity)JsonSerializer.Deserialize(jsonNode["data"], entityType, options)!;
                var segment = new MilkySegment
                {
                    Type = type,
                    Entity = data
                };
                return segment;
            }
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, MilkySegment value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("type");
        writer.WriteStringValue(value.Type);
        writer.WritePropertyName("data");
        JsonSerializer.Serialize(writer, value.Entity, options);

        writer.WriteEndObject(); // 结束 JSON 对象
    }

}
