using SunSet.Core.Common.ApiResultArgs;
using SunSet.Core.Enumerates;
using SunSet.Core.Milky.Info;
using SunSet.Core.Operation.Message;
using SunSet.Core.Segments;
using System.ComponentModel;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common;

public class ApiRequestHandler : IDisposable
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

    #region File API
    public async Task<ApiResult<UploadFileResult>> UploadPrivateFiles(uint userid, string fileUrl, CancellationToken cancellationToken = default)
    {
        return await Request<UploadFileResult>(new
        {
            user_id = userid,
            file_url = fileUrl
        }, ApiOperationType.UPLOAD_PRIVATE_FILE, cancellationToken);
    }

    public async Task<ApiResult<UploadFileResult>> UploadGroupFiles(uint groupid, string fileUrl, CancellationToken cancellationToken = default)
    {
        return await Request<UploadFileResult>(new
        {
            group_id = groupid,
            file_url = fileUrl
        }, ApiOperationType.UPLOAD_GROUP_FILE, cancellationToken);
    }

    public async Task<ApiResult<FileDownloadUrlResult>> GetPrivateDownloadUrl(uint userid, string fileid, CancellationToken cancellationToken = default)
    {
        return await Request<FileDownloadUrlResult>(new
        {
            user_id = userid,
            file_id = fileid
        }, ApiOperationType.GET_PRIVATE_FILE_DOWNLOAD_URL, cancellationToken);
    }

    public async Task<ApiResult<FileDownloadUrlResult>> GetGroupDownloadUrl(uint groupid, string fileid, CancellationToken cancellationToken = default)
    {
        return await Request<FileDownloadUrlResult>(new
        {
            group_id = groupid,
            file_id = fileid
        }, ApiOperationType.GET_GROUP_FILE_DOWNLOAD_URL, cancellationToken);
    }

    public async Task<ApiResult<GetGroupFilesResult>> GetGroupFiles(uint groupid, string parentFolderId, CancellationToken cancellationToken = default)
    {
        return await Request<GetGroupFilesResult>(new
        {
            group_id = groupid,
            parent_folder_id = parentFolderId
        }, ApiOperationType.GET_GROUP_FILES, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> DeleteGroupFile(uint groupid, string fileid, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            file_id = fileid
        }, ApiOperationType.DELETE_GROUP_FILE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> RenameGroupFile(uint groupid, string fileid, string name, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            file_id = fileid,
            new_name = name
        }, ApiOperationType.RENAME_GROUP_FILE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> MoveGroupFile(uint groupid, string fileid, string targetFolderId, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            file_id = fileid,
            target_folder_id = targetFolderId
        }, ApiOperationType.MOVE_GROUP_FILE, cancellationToken);
    }

    public async Task<ApiResult<CreateFolderResult>> CreateGroupFolder(uint groupid, string folderName, CancellationToken cancellationToken = default)
    {
        return await Request<CreateFolderResult>(new
        {
            group_id = groupid,
            folder_name = folderName,
        }, ApiOperationType.CREATE_GROUP_FOLDER, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> RenameGroupFolder(uint groupid, string folderId, string newName, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            folder_id = folderId,
            new_name = newName
        }, ApiOperationType.RENAME_GROUP_FOLDER, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> DeleteGroupFolder(uint groupid, string folderId, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            folder_id = folderId,
        }, ApiOperationType.DELETE_GROUP_FOLDER, cancellationToken);
    }
    #endregion

    #region Request API
    public async Task<ApiResult<JsonNode>> AcceptRequest(string requestid, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new { request_id = requestid }, ApiOperationType.ACCEPT_REQUEST, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> RejectRequest(string requestid, string reasons, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            request_id = requestid,
            reasons
        }, ApiOperationType.REJECT_REQUEST, cancellationToken);
    }
    #endregion

    #region Group API
    public async Task<ApiResult<JsonNode>> SetGroupName(uint groupid, string name, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            name
        }, ApiOperationType.SET_GROUP_NAME, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SetGroupAvatar(uint groupid, string imageUrl, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            image_url = imageUrl
        }, ApiOperationType.SET_GROUP_AVATAR, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SetGroupMemberCard(uint groupid, uint userid, string card, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            user_id = userid,
            card
        }, ApiOperationType.SET_GROUP_MEMBER_CARD, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SetGroupMemberSpecialTitle(uint groupid, uint userid, string title, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            special_title = title,
            user_id = userid
        }, ApiOperationType.SET_GROUP_MEMBER_SPECIAL_TITLE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SetGroupMemberAdmin(uint groupid, uint userid, bool isSet, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            user_id = userid,
            is_set = isSet
        }, ApiOperationType.SET_GROUP_MEMBER_ADMIN, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SetGroupMemberMute(uint groupid, uint userid, int duration, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            user_id = userid,
            duration
        }, ApiOperationType.SET_GROUP_MEMBER_MUTE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SetGroupWholeMute(uint groupid, bool isBan, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            is_mute = isBan
        }, ApiOperationType.SET_GROUP_WHOLE_MUTE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> KickGroupMember(uint groupid, uint userid, bool isBan, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            user_id = userid,
            reject_add_request = isBan
        }, ApiOperationType.KICK_GROUP_MEMBER, cancellationToken);
    }

    public async Task<ApiResult<List<GroupAnnouncementResult>>> GetGroupAnnouncementList(uint groupid, CancellationToken cancellationToken = default)
    {
        return await Request<List<GroupAnnouncementResult>>(new
        {
            group_id = groupid
        }, ApiOperationType.GET_GROUP_ANNOUNCEMENT_LIST, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SendGroupAnnouncement(uint groupid, string content, string imageUrl, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            content,
            image_url = imageUrl
        }, ApiOperationType.SEMD_GROUP_ANNOUNCEMENT, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> DeleteGroupAnnouncement(uint groupid, string announcementId, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            announcement_id = announcementId
        }, ApiOperationType.DELETE_GROUP_ANNOUNCEMENT, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SendGroupMessageReaction(uint groupid, long messageId, string reaction, bool isAdd, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            message_seq = messageId,
            reaction,
            is_add = isAdd
        }, ApiOperationType.SEND_GROUP_MESSAGE_REACTION, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SendGroupNudge(uint groupid, uint userid, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            user_id = userid
        }, ApiOperationType.SEND_GROUP_NUDGE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> QuitGroup(uint groupid, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid
        }, ApiOperationType.QUIT_GROUP, cancellationToken);
    }
    #endregion

    #region Friend API
    public async Task<ApiResult<JsonNode>> SendFriendNudge(uint userid, bool isSelf, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            user_id = userid,
            is_self = isSelf
        }, ApiOperationType.SEND_FRIEND_NUDGE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> SendProfileLike(uint userid, int count, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            user_id = userid,
            count
        }, ApiOperationType.SEND_PROFILE_LIKE, cancellationToken);
    }
    #endregion

    #region Message API
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

    public async Task<ApiResult<MilkyFriendMessage>> GetFriendMessage(long groupUin, long messageId, CancellationToken cancellationToken = default)
    {
        return await Request<MilkyFriendMessage>(new
        {
            message_scene = "friend",
            peer_id = groupUin,
            message_seq = messageId
        }, ApiOperationType.GET_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<MilkyGroupMessage>> GetHistoryGroupMessage(long groupUin, long starMessageId, string direction, int limit, CancellationToken cancellationToken = default)
    {
        return await Request<MilkyGroupMessage>(new
        {
            message_scene = "group",
            peer_id = groupUin,
            start_message_seq = starMessageId,
            direction,
            limit,
        }, ApiOperationType.GET_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<MilkyFriendMessage>> GetHistoryFriendMessage(long friendUin, long starMessageId, string direction, int limit, CancellationToken cancellationToken = default)
    {
        return await Request<MilkyFriendMessage>(new
        {
            message_scene = "friend",
            peer_id = friendUin,
            start_message_seq = starMessageId,
            direction,
            limit,
        }, ApiOperationType.GET_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<TempResourceResult>> GetTempResourceUrl(string resourceId, CancellationToken cancellationToken = default)
    {
        return await Request<TempResourceResult>(new
        {
            resource_id = resourceId
        }, ApiOperationType.GET_RESOURCE_TEMP_URL, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> RecallPrivateMessage(uint userid, long messageid, long clientId, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            user_id = userid,
            message_seq = messageid,
            client_seq = clientId
        }, ApiOperationType.RECALL_PRIVATE_MESSAGE, cancellationToken);
    }

    public async Task<ApiResult<JsonNode>> RecallGroupMessage(uint groupid, long messageid, CancellationToken cancellationToken = default)
    {
        return await Request<JsonNode>(new
        {
            group_id = groupid,
            message_seq = messageid
        }, ApiOperationType.RECALL_GROUP_MESSAGE, cancellationToken);
    }
    #endregion

    #region System API
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
    #endregion

    private async Task<ApiResult<T>> Request<T>(object? obj, ApiOperationType type, CancellationToken cancellationToken = default)
    {
        var api = type.GetType().GetField(type.ToString())?
            .GetCustomAttribute<DescriptionAttribute>()?.Description
            ?? throw new InvalidOperationException("API type not found.");
        // Serialize the object to JSON
        var json = obj == null ? "{}" : JsonSerializer.Serialize(obj);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        // Set the content type header
        var response = await _client.PostAsync(BaseUrl + api, content, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API request failed with status code {response.StatusCode}: {await response.Content.ReadAsStringAsync(cancellationToken)}");
        }
        var resultString = await response.Content.ReadAsStringAsync(cancellationToken);

        // Deserialize the response into ApiResult<T>
        var apiResult = JsonSerializer.Deserialize<ApiResult<T>>(resultString, CachedJsonSerializerOptions)
            ?? throw new InvalidOperationException("API response deserialization failed.");
        _context.Log.LogDebug($"[{nameof(ApiRequestHandler)}]: API Request: {api}, Payload: {json}, Status Code: {response.StatusCode}, Response: {apiResult}");
        return apiResult;
    }

    public void Dispose()
    {
        _client?.Dispose();
        _context.Log.LogDebug($"[{nameof(ApiRequestHandler)}]: Disposed API Request Handler.");
        GC.SuppressFinalize(this);
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
        return $"Code: {Code}, Msg: {Status}, Data: {ToPreviewString()}";
    }

    private string ToPreviewString()
    {
        return Data switch
        {
            null => "null",
            string str => str,
            IEnumerable<object> enumerable => $"Count: {enumerable.Count()} {string.Join(", ", enumerable.Take(3).Select(item => item?.ToString() ?? "null"))}...",
            _ => Data.ToString() ?? "null"
        };
    }
}
