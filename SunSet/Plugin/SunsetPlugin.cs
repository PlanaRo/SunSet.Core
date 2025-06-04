using Microsoft.Extensions.Logging;
using SunSet.ConfigFiles;
using SunSet.Core;

namespace SunSet.Plugin;

public abstract class SunsetPlugin(BotContext context, ILogger logger) : IDisposable
{
    public virtual string Name
    {
        get => GetType().Name;
    }

    public virtual string Author
    {
        get => "None";
    }

    public virtual string Description
    {
        get => "this is a Plugin";
    }

    public virtual Version Version
    {
        get => new(1, 0);
    }

    public int Order { get; set; }

    protected BotContext BotContext { get; } = context;

    protected ILogger Logger { get; } = logger;

    private void AutoLoad()
    {
        SunsetAPI.CommandManager.RegisterCommand(GetType().Assembly);
        foreach (var type in GetType().Assembly.GetTypes().Where(t => t.IsDefined(typeof(ConfigSeriesAttribute), true)))
        {
            var method = type.BaseType!.GetMethod("Load") ?? throw new MissingMethodException($"method 'Load()' is missing inside the lazy loaded config class '{Name}'");
            var name = method.Invoke(null, []);
            Logger.LogInformation("[PluginLoader] Config Registered Successfully: {name}", name);
        }
    }

    protected abstract void Initialize();

    internal void OnInitialize()
    {
        AutoLoad();
        Initialize();
    }

    protected abstract void Dispose(bool dispose);

    public void Dispose()
    {
        SunsetAPI.CommandManager.UnRegisterCommand(GetType().Assembly);
        foreach (var type in GetType().Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(JsonConfigBase<>))))
        {
            var method = type.BaseType!.GetMethod("UnLoad") ?? throw new MissingMethodException($"method 'UnLoad()' is missing inside the lazy loaded config class '{Name}'");
            var name = method.Invoke(null, []);
            Logger.LogInformation("[PluginLoader] Config UnRegistered Successfully: {name}", name);
        }
        GC.SuppressFinalize(this);
    }
}
