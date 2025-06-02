using System.Text.Json.Serialization;

namespace SunSet.Core.Milky.Info;

public class FriendCategory
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; } = string.Empty;
}
