namespace SunSet.Plugin;

public class PluginContainer(SunsetPlugin plugin)
{
    public SunsetPlugin Plugin { get; protected set; } = plugin;

    public bool Initialized { get; protected set; }

    public void DeInitialize()
    {
        Initialized = false;
        Plugin.Dispose();
    }

    public void Initialize()
    {
        Initialized = true;
        Plugin.OnInitialize();
    }
}
