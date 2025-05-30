using System.ComponentModel;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using SunSet.Core.Common.ApiResultArgs;
using SunSet.Core.Enumerates;
using SunSet.Core.Milky.Info;
using SunSet.Core.Milky.Message;
using SunSet.Core.Segments;

namespace SunSet.Core.Common;

public class ApiRequestHandler
{
    private readonly BotContext _context;

    private readonly string BaseUrl;

    private readonly HttpClient _client = new();

    // Add a static readonly field to cache the JsonSerializerOptions instance
    private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public ApiRequestHandler(BotContext context)
    {
        _context = context;
        BaseUrl = $"http://{_context.Config.Host}:{_context.Config.Port}/api/";
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_context.Config.AccessToken}");
    }

    // 修改所有公开异步方法，添加 CancellationToken 参数（带默认值）
    public async Task<ApiResult<GroupMessageResult>> SendGroupMsg(MessageChain chain, CancellationToken cancellationToken = default)
    {
        return await Request<GroupMessageResult>(new
        {
            group_id = chain.GroupUin,
            message = chain
        }, ApiOperationType.SEND_GROUP_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<PrivateMessageResult>> SendPrivateMsg(MessageChain chain, CancellationToken cancellationToken = default)
    {
        return await Request<PrivateMessageResult>(new
        {
            user_id = chain.UserUin,
            message = chain
        }, ApiOperationType.SEND_PRIVATE_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<MilkyGroupMessage>> GetGroupMessage(long groupUin, long messageId, CancellationToken cancellationToken = default)
    {
        return await Request<MilkyGroupMessage>(new 
        {
            message_scene = "group",
            peer_id = groupUin,
            message_seq = messageId 
        }, ApiOperationType.GET_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<UserLoginDetails>> GetLoginInfo(CancellationToken cancellationToken = default)
    {
        return await Request<UserLoginDetails>(null, ApiOperationType.GET_LOGIN_INFO, cancellationToken);
    }

    public async Task<ApiResult<List<Friend>>> GetFriends(bool nocache, CancellationToken cancellationToken = default)
    {
        return await Request<List<Friend>>(new { no_cache = nocache }, ApiOperationType.GET_FRIEND_LIST, cancellationToken);
    }

    public async Task<ApiResult<Friend>> GetFriendInfo(uint userid, bool nocache, CancellationToken cancellationToken = default)
    {
        return await Request<Friend>(new
        {
            no_cache = nocache,
            user_id = userid
        }, ApiOperationType.GET_FFRIEND_INFO, cancellationToken);
    }

    public async Task<ApiResult<List<Group>>> GetGroups(bool nocache, CancellationToken cancellationToken = default)
    {
        return await Request<List<Group>>(new { no_cache = nocache }, ApiOperationType.GET_GROUP_LIST, cancellationToken);
    }

    public async Task<ApiResult<Group>> GetGroupInfo(uint groupid, bool nocache, CancellationToken cancellationToken = default)
    {
        return await Request<Group>(new
        {
            no_cache = nocache,
            group_id = groupid
        }, ApiOperationType.GET_GROUP_INFO, cancellationToken);
    }

    public async Task<ApiResult<List<GroupMember>>> GetGroupMembers(uint groupid, bool nocache, CancellationToken cancellationToken = default)
    {
        return await Request<List<GroupMember>>(new
        {
            no_cache = nocache,
            group_id = groupid
        }, ApiOperationType.GET_GROUP_MEMBER_LIST, cancellationToken);
    }

    public async Task<ApiResult<GroupMember>> GetGroupMemberInfo(uint groupid, uint userid, bool nocache, CancellationToken cancellationToken = default)
    {
        return await Request<GroupMember>(new
        {
            no_cache = nocache,
            group_id = groupid,
            user_id = userid
        }, ApiOperationType.GET_GROUP_MEMBER_INFO, cancellationToken);
    }

    // 修改 Request<T> 方法，添加 CancellationToken 参数
    private async Task<ApiResult<T>> Request<T>(object? obj, ApiOperationType type, CancellationToken cancellationToken = default)
    {
        var api = type.GetType().GetField(type.ToString())!
            .GetCustomAttribute<DescriptionAttribute>()?.Description ?? throw new InvalidOperationException("API type not found.");
        var json = obj == null ? "{}" : JsonSerializer.Serialize(obj);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var result = await _client.PostAsync(BaseUrl + api, content, cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API request failed with status code {result.StatusCode}: {await result.Content.ReadAsStringAsync(cancellationToken)}");
        }
        var response = await result.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ApiResult<T>>(response, CachedJsonSerializerOptions)
            ?? throw new InvalidOperationException("API response deserialization failed.");
    }
}


public class ApiResult<T>
{
    [JsonPropertyName("retcode")]
    public int Code { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;

    public bool IsSuccess => Code == 0;

    public override string ToString()
    {
        return $"Code: {Code}, Msg: {Status}, Data: {Data}";
    }
}
