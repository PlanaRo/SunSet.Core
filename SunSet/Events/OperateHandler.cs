using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SunSet.Commands;
using SunSet.Database.Managers;
using SunSet.Enumerates;
using SunSet.EventArgs;

namespace SunSet.Events;

public class OperateHandler
{
    public delegate IResult OperateEventHandler<IArgs, IResult>(IArgs args);

    public static event OperateEventHandler<PermissionEventArgs, UserPermissionType>? OnPermission;

    public static event OperateEventHandler<CommandArgs, Task>? OnCommand;

    public static UserPermissionType PermissionEvent(Account account, string perm)
    {
        var ownerId = SunsetAPI.ServiceProvider.GetRequiredService<IConfiguration>().GetSection("BotSettings:Owners").Get<HashSet<long>>() ?? [];
        if (ownerId.Contains(account.UserId))
            return UserPermissionType.Granted;
        if (OnPermission == null)
            return UserPermissionType.Denied;
        var args = new PermissionEventArgs(account, perm, UserPermissionType.Denied);
        return OnPermission(args);
    }

    public static async Task<bool> CommandEvent(CommandArgs args)
    {
        if (OnCommand == null)
            return false;
        await OnCommand(args);
        return args.Handler;
    }
}
