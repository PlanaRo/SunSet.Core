using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SunSet.Commands;
using SunSet.Core;

namespace SunSet.Plugin;

public class PluginContext(string name) : AssemblyLoadContext(name, true)
{
    public readonly List<Assembly> LoadAssemblys = [];

    public readonly List<PluginContainer> Plugins = [];

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        foreach (Assembly assembly in LoadAssemblys)
        {
            if (assembly.GetName() == assemblyName)
                return assembly;
        }
        return Default.LoadFromAssemblyName(assemblyName);
    }

    public void LoadPlugins(DirectoryInfo dir, ILogger<PluginLoader> logger, BotContext bot)
    {
        foreach (FileInfo file in dir.GetFiles("*.dll", SearchOption.AllDirectories))
        {
            using FileStream stream = file.OpenRead();
            using FileStream? pdbStream = File.Exists(Path.ChangeExtension(file.FullName, ".pdb")) ? File.OpenRead(Path.ChangeExtension(file.FullName, ".pdb")) : null;
            Assembly assembly = LoadFromStream(stream, pdbStream);
            LoadAssemblys.Add(assembly);
        }
        foreach (Assembly assembly in LoadAssemblys)
        {
            foreach (Type type in assembly.GetExportedTypes())
            {
                if (type.IsSubclassOf(typeof(SunsetPlugin)) && !type.IsAbstract)
                {
                    var log = SunsetAPI.ServiceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(type)) as ILogger ?? logger;
                    if (Activator.CreateInstance(type, bot, log) is SunsetPlugin instance)
                        Plugins.Add(new(instance));
                }
            }
        }

        foreach (var p in Plugins.OrderBy(p => p.Plugin.Order))
        { 
            logger.LogInformation("[{Time}] [PluginLoader] Plugin {PluginName} V{PluginVersion} by({PluginAuthor}) Initiate.", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), p.Plugin.Name, p.Plugin.Version, p.Plugin.Author);
            p.Initialize();
        }
    }

    public void UnloadPlugin()
    {
        Plugins.ForEach(x => x.DeInitialize());
        Plugins.Clear();
        LoadAssemblys.Clear();
        Unload();
    }
}
