using Microsoft.Extensions.Logging;
using SunSet.Database.Managers;
using SunSet.Extensions;
using System.Text;

namespace SunSet.Commands;

public class GroupCommand : Command
{
    public override string[] Aliases { get; set; } = ["group", "g"];
    public override string[] Permissions { get; set; } = ["sunset.command.group"];
    public override string Description { get; set; } = "管理用户组和权限。";

    public override string ErrorText { get; set; } = "命令错误，请使用:/group help查看详情";

    [SubCommand("help")]
    public static async Task GroupHelp(CommandArgs args, ILogger logger)
    {
        var sb = new StringBuilder("用户组管理命令:\n");
        sb.AppendLine("/group add <group_name> [parent_group] [permissions] - 添加新用户组");
        sb.AppendLine("/group list - 列出所有用户组");
        sb.AppendLine("/group remove <group_name> - 删除指定用户组");
        sb.AppendLine("/group addperm <group_name> <permissions> - 为用户组添加权限");
        sb.AppendLine("/group delperm <group_name> <permissions> - 从用户组中删除权限");
        sb.AppendLine("/group parent <group_name> <parent_group> - 设置用户组的父组");
        await args.Reply(sb.ToString().Trim());
    }

    [SubCommand("add", 2), HelpText("/group add <group_name> [parent_group] [permissions]")]
    public static async Task AddGroup(CommandArgs args, ILogger logger)
    {
        var groupName = args.Parameters[1];
        var parentGroup = args.Parameters.Count > 2 ? args.Parameters[2] : null;
        var permissions = args.Parameters.Count > 3 ? args.Parameters[3] : string.Empty;
        try
        {
            Group.InsertGroup(groupName, parentGroup, permissions);
            await args.Reply($"用户组 '{groupName}' 已成功添加。");
        }
        catch (Exception ex)
        {
            await args.Reply($"添加用户组失败: {ex.Message}");
        }
    }

    [SubCommand("list")]
    public static async Task GroupList(CommandArgs args, ILogger logger)
    {
        var groups = Group.GetAllGroups();
        if (groups.Count == 0)
        {
            await args.Reply("当前没有任何用户组。");
            return;
        }
        var sb = new StringBuilder("当前用户组列表:\n");
        foreach (var group in groups)
        {
            sb.AppendLine($"- {group.Name} (父组: {group.ParentGroup}, 权限: {group.Permission})");
        }
        
        await args.Reply(sb.ToString());
    }

    [SubCommand("remove", 2), HelpText("/group remove <group_name>")]
    public static async Task RemoveGroup(CommandArgs args, ILogger logger)
    {
        var groupName = args.Parameters[1];
        try
        {
            Group.DeleteGroup(groupName);
            await args.Reply($"用户组 '{groupName}' 已成功删除。");
        }
        catch (Exception ex)
        {
            await args.Reply($"删除用户组失败: {ex.Message}");
        }
    }

    [SubCommand("addperm", 3), HelpText("/group addperm <group_name> <permissions>")]
    public static async Task AddGroupPermissions(CommandArgs args, ILogger logger)
    {
        var groupName = args.Parameters[1];
        var permissions = args.Parameters[2];
        try
        {
            Group.AddPermission(groupName, permissions);
            await args.Reply($"用户组 '{groupName}' 的权限已更新。");
        }
        catch (Exception ex)
        {
            await args.Reply($"更新用户组权限失败: {ex.Message}");
        }
    }

    [SubCommand("delperm", 3), HelpText("/group delperm <group_name> <permissions>")]
    public static async Task RemoveGroupPermissions(CommandArgs args, ILogger logger)
    {
        var groupName = args.Parameters[1];
        var permissions = args.Parameters[2];
        try
        {
            Group.RemovePermission(groupName, permissions);
            await args.Reply($"用户组 '{groupName}' 的权限已更新。");
        }
        catch (Exception ex)
        {
            await args.Reply($"更新用户组权限失败: {ex.Message}");
        }
    }

    [SubCommand("parent", 3), HelpText("/group parent <group_name> <parent_group>")]
    public static async Task SetParentGroup(CommandArgs args, ILogger logger)
    {
        var groupName = args.Parameters[1];
        var parentGroup = args.Parameters[2];
        try
        {
            Group.SetParentGroup(groupName, parentGroup);
            await args.Reply($"用户组 '{groupName}' 的父组已设置为 '{parentGroup}'。");
        }
        catch (Exception ex)
        {
            await args.Reply($"设置用户组父组失败: {ex.Message}");
        }
    }

    [SubCommand("listperm", 2), HelpText("/group listperm <group_name>")]
    public static async Task GroupPermList(CommandArgs args, ILogger logger)
    {
        if (Group.GetGroup(args.Parameters[1]) is Group group)
        {
            var perm = $"{group.Name}权限列表:{group.GroupPermissions.JoinToString(x => x, ",")}";
            await args.Reply(perm);
            return;
        }
        await args.Reply($"组`{args.Parameters[1]}`不存在!");
    }
}
