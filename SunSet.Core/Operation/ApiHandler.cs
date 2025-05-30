using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using SunSet.Core.Enumerates;
using SunSet.Core.Segments;
using SunSet.Core.Utils;

namespace SunSet.Core.Operation;

public class ApiHandler
{
    private readonly BotContext _context;

    private string BaseUrl;

    private readonly HttpClient _client = new();

    public ApiHandler(BotContext context)
    {
        _context = context;
        BaseUrl = $"http://{_context.Config.Host}:{_context.Config.Port}/api/";
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_context.Config.AccessToken}");
    }

    public async Task SendGroupMsg(MessageChain chain)
    { 
        var obj = new
        {
            group_id = chain.GroupUin,
            message = chain
        };
        var result = await Request(obj, ApiType.SEND_GROUP_MESSAGE);
        Console.WriteLine(result);
    }

    public async Task<(string userName, uint userUin)> GetLoginInfo()
    { 
        return await Request(null, ApiType.GET_LOGIN_INFO)
            .ContinueWith(task =>
            {
                var json = task.Result;
                var data = JsonSerializer.Deserialize<JsonElement>(json);
                var userName = data.GetProperty("data").GetProperty("nickname").GetString() ?? string.Empty;
                var userUin = data.GetProperty("data").GetProperty("uin").GetUInt32();
                return (userName, userUin);
            });
    }

    private async Task<string> Request(object? obj, ApiType type)
    { 
        var api = type.GetType().GetField(type.ToString())!
            .GetCustomAttribute<DescriptionAttribute>()?.Description ?? throw new InvalidOperationException("API type not found.");
        var json = obj == null ? "{}" : JsonSerializer.Serialize(obj);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var result = await _client.PostAsync(BaseUrl + api, content);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API request failed with status code {result.StatusCode}: {await result.Content.ReadAsStringAsync()}");
        }
        return await result.Content.ReadAsStringAsync();
    }
}
