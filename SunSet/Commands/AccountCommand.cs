using Microsoft.Extensions.Logging;
using SunSet.Database.Managers;
using System.Text;

namespace SunSet.Commands;

public class AccountCommand : Command
{
    public override string[] Aliases => ["account", "acc"];

    public override string[] Permissions => ["sunset.account.manage"];

    public override string Description => "管理用户账号和权限。";

    public override string ErrorText => "账号命令出错，请使用正确的语法！";

    [SubCommand("help"), HelpText("/account help")]
    public static async Task HelpAsync(CommandArgs args, ILogger logger)
    {
        var helpText = new StringBuilder("账号命令帮助：\n");
        helpText.AppendLine("/account add <group> <userid> - 将用户账号添加到指定分组。");
        helpText.AppendLine("/account del <userid> - 删除用户账号。");
        helpText.AppendLine("/account group <userid> <group> - 将用户账号移动到其他分组。");
        helpText.AppendLine("/account list - 列出所有用户账号。");
        await args.Reply(helpText.ToString().Trim());
    }


    [SubCommand("add", 3), HelpText("/account add <group> <userid>")]
    public static async Task AddAccountAsync(CommandArgs args, ILogger logger)
    {
        if (args.Parameters.Count < 3)
        {
            await args.Reply("用法：/account add <group> <userid>");
            return;
        }
        var groupName = args.Parameters[1];
        if (Group.GetGroup(groupName) == null)
        {
            await args.Reply($"分组 '{groupName}' 不存在。");
            return;
        }
        if (!uint.TryParse(args.Parameters[2], out var userId))
        {
            await args.Reply("用户ID格式无效。");
            return;
        }
        Account.InsertAccount(userId, groupName);
        await args.Reply($"已将用户 {userId} 添加到分组 '{groupName}'。");
    }

    [SubCommand("del", 2), HelpText("/account del <userid>")]
    public static async Task DeleteAccountAsync(CommandArgs args, ILogger logger)
    {
        if (args.Parameters.Count < 2)
        {
            await args.Reply("用法：/account del <userid>");
            return;
        }
        if (!uint.TryParse(args.Parameters[1], out var userId))
        {
            await args.Reply("用户ID格式无效。");
            return;
        }
        try
        {
            Account.DeleteAccount(userId);
            await args.Reply($"已删除用户 {userId} 的账号。");
        }
        catch (NullReferenceException)
        {
            await args.Reply($"用户 {userId} 的账号不存在。");
        }
    }

    [SubCommand("group", 3), HelpText("/account group <userid> <group>")]
    public static async Task MoveAccountAsync(CommandArgs args, ILogger logger)
    {
        if (!uint.TryParse(args.Parameters[1], out var userId))
        {
            await args.Reply("用户ID格式无效。");
            return;
        }
        var groupName = args.Parameters[2];
        if (Group.GetGroup(groupName) == null)
        {
            await args.Reply($"分组 '{groupName}' 不存在。");
            return;
        }
        try
        {
            Account.MoveAccountGroup(userId, groupName);
            await args.Reply($"已将用户 {userId} 移动到分组 '{groupName}'。");
        }
        catch (NullReferenceException)
        {
            await args.Reply($"用户 {userId} 的账号不存在。");
        }
    }

    [SubCommand("list"), HelpText("/account list")]
    public static async Task ListAccountsAsync(CommandArgs args, ILogger logger)
    {
        var accounts = Account.AccountContext.Records.ToList();
        if (accounts.Count == 0)
        {
            await args.Reply("未找到任何账号。");
            return;
        }
        var sb = new StringBuilder("账号列表：\n");
        foreach (var account in accounts)
        {
            sb.AppendLine($"用户ID: {account.UserId}, 分组: {account.GroupName}");
        }
        await args.Reply(sb.ToString().Trim());
    }
}
