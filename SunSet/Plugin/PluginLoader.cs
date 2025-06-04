using Microsoft.Extensions.Logging;
using SunSet;
using SunSet.Core;

namespace SunSet.Plugin;

public class PluginLoader(BotContext botContext, ILogger<PluginLoader> logger)
{

    private BotContext BotContext { get; } = botContext;

    private ILogger<PluginLoader> Logger { get; } = logger;

    public PluginContext PluginContext { get; private set; } = new(Guid.NewGuid().ToString());

    /// <summary>
    /// 加载插件
    /// </summary>
    public void Load()
    {
        DirectoryInfo directoryInfo = new(SunsetAPI.PLUGIN_PATH);
        if (!directoryInfo.Exists)
            directoryInfo.Create();
        PluginContext.LoadPlugins(directoryInfo, Logger, BotContext);
    }

    public void UnLoad()
    {
        PluginContext.UnloadPlugin();
        PluginContext = new(Guid.NewGuid().ToString());
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
