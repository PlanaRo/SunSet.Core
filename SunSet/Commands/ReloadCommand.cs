using Microsoft.Extensions.Logging;
using SunSet.EventArgs;
using SunSet.Events;

namespace SunSet.Commands;

public class ReloadCommand : Command
{
    public override string[] Aliases => ["reload", "rl"];
    public override string[] Permissions => ["sunset.reload"];
    public override string Description => "重新加载服务器配置和数据。";
    public override string ErrorText => "重载命令出错，请使用正确的语法！";

    public override async Task ExecuteAsync(CommandArgs args, ILogger logger)
    {
        var reloadArgs = new ReloadEventArgs(args.Message.Group.GroupUin);
        try
        {
            await OperateHandler.ReloadEvent(reloadArgs);
            await args.Reply("服务器配置和数据已成功重新加载。");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "执行重载命令时出错。");
            await args.Reply($"重载时发生错误：{ex.Message}");
        }
    }
}
