using Microsoft.Extensions.Logging;
using SunSet;
using SunSet.Commands;
using SunSet.Core;
using SunSet.Core.Log;
using SunSet.Enumerates;
using SunSet.EventArgs;
using SunSet.Events;
using SunSet.Plugin;

namespace PluginExample;

public class Plugin : SunsetPlugin
{
    public override string Name => "PluginExample";

    public override string Description => "这是一个示例插件，用于展示SunSet插件系统的基本用法。";

    public override Version Version => new(1, 0, 0);

    public override string Author => "Your Name";

    private string LogPath => Path.Combine(SunsetAPI.PATH, "Logs");

    private StreamWriter _logWriter;

    public Plugin(BotContext context, ILogger logger) : base(context, logger)
    {
        if(!Directory.Exists(LogPath))
        {
            Directory.CreateDirectory(LogPath);
        }
        var logfile = Path.Combine(LogPath, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log");
        _logWriter = new StreamWriter(logfile, append: true) { AutoFlush = true };
    }

    /// <summary>
    /// 销毁插件时调用的方法。
    /// </summary>
    /// <param name="dispose"></param>
    /// <exception cref="NotImplementedException"></exception>
    protected override void Dispose(bool dispose)
    {
        if (dispose)
        { 
            _logWriter.Dispose(); // 释放日志文件写入器资源
            OperateHandler.OnCommand -= OnCommand;
            OperateHandler.OnPermission -= OnPermission;
        }
    }

    /// <summary>
    /// 入口函数，用于初始化插件。
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected override void Initialize()
    {
        BotContext.Invoke.BotLogEvent += OnLogger;
        OperateHandler.OnCommand += OnCommand;
        OperateHandler.OnPermission += OnPermission;
    }

    /// <summary>
    /// 机器人日志事件处理器，记录日志到文件。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task OnLogger(BotContext context, BotLogEventArgs args) =>
        await _logWriter.WriteLineAsync($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{args.Level}]: {args.Message}");

    /// <summary>
    /// 权限事件处理器，检查权限是否被禁用。
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private UserPermissionType OnPermission(PermissionEventArgs args)
    {
        if(Config.Instance.DisabledPermissions.Contains(args.Permission.ToLower()))
        {
            return UserPermissionType.Unhandled; // 直接赋予无权限状态
        }
        return UserPermissionType.Denied; // 默认返回未处理状态，允许其他插件或系统处理权限逻辑
    }

    /// <summary>
    /// 拦截命令事件，检查命令是否被禁用。
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private async Task OnCommand(CommandArgs args)
    {
        if(Config.Instance.DisabledCommands.Contains(args.CommandName.ToLower()))
        {
            await args.Reply("此命令已被禁用。");
            args.Handler = true; // 阻止进一步处理
            return;
        }
    }
}
