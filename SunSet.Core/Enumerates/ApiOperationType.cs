using System.ComponentModel;

namespace SunSet.Core.Enumerates;

public enum ApiOperationType
{
    #region System API
    [Description("get_login_info")]
    GET_LOGIN_INFO,

    [Description("get_group_list")]
    GET_GROUP_LIST,

    [Description("get_group_info")]
    GET_GROUP_INFO,

    [Description("get_friend_list")]
    GET_FRIEND_LIST,

    [Description("get_friend_info")]
    GET_FFRIEND_INFO,

    [Description("get_group_member_list")]
    GET_GROUP_MEMBER_LIST,

    [Description("get_group_member_info")]
    GET_GROUP_MEMBER_INFO,
    #endregion

    #region Message API
    [Description("send_group_message")]
    SEND_GROUP_MESSAGE,

    [Description("send_private_message")]
    SEND_PRIVATE_MESSAGE,

    [Description("get_message")]
    GET_MESSAGE,

    [Description("get_history_messages")]
    GET_HISTORY_MESSAGES,

    [Description("get_resource_temp_url")]
    GET_RESOURCE_TEMP_URL,

    [Description("get_forwarded_messages")]
    GET_FORWARDED_MESSAGES,

    [Description("recall_private_message")]
    RECALL_PRIVATE_MESSAGE,

    [Description("recall_group_message")]
    RECALL_GROUP_MESSAGE,
    #endregion

    #region Friend API
    [Description("send_friend_nugde")]
    SEND_FRIEND_NUDGE,

    [Description("send_profile_like")]
    SEND_PROFILE_LIKE,
    #endregion

    #region Group API
    [Description("set_group_name")]
    SET_GROUP_NAME,

    [Description("set_group_avatar")]
    SET_GROUP_AVATAR,

    [Description("set_group_member_card")]
    SET_GROUP_MEMBER_CARD,

    [Description("set_group_member_special_title")] 
    SET_GROUP_MEMBER_SPECIAL_TITLE,

    [Description("set_group_member_admin")]
    SET_GROUP_MEMBER_ADMIN,

    [Description("set_group_member_mute")]
    SET_GROUP_MEMBER_MUTE,

    [Description("set_group_whole_mute")]
    SET_GROUP_WHOLE_MUTE,

    [Description("kick_group_member")]
    KICK_GROUP_MEMBER,

    [Description("get_group_announcement_list")]
    GET_GROUP_ANNOUNCEMENT_LIST,

    [Description("send_group_announcement")]
    SEMD_GROUP_ANNOUNCEMENT,

    [Description("delete_group_announcement")]
    DELETE_GROUP_ANNOUNCEMENT,

    [Description("quit_group")]
    QUIT_GROUP,

    [Description("send_group_message_reaction")]
    SEND_GROUP_MESSAGE_REACTION,

    [Description("send_group_nudge")]
    SEND_GROUP_NUDGE,
    #endregion

    #region Request API
    [Description("reject_request")]
    REJECT_REQUEST,

    [Description("accept_request")]
    ACCEPT_REQUEST
    #endregion
}
