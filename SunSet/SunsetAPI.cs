using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SunSet.Commands;
using SunSet.Core;
using SunSet.Plugin;
using System.Data;
using System.Reflection;

namespace SunSet;

public class SunsetAPI : IHostedService
{
    public static string PATH => Environment.CurrentDirectory;

    public static string PLUGIN_PATH => Path.Combine(PATH, "Plugins");

    public static string CONFIG_PATH => Path.Combine(PATH, "Config");

    public static IServiceProvider ServiceProvider => SunSetApp.ServiceProvider;

#nullable disable

    public static BotContext BotContext { get; private set; }

    public static ILogger<SunsetAPI> Logger { get; private set; }

    public static CommandManager CommandManager { get; private set; }

    public static PluginLoader PluginLoader { get; private set; }

    internal static IDbConnection DB { get; private set; }

#nullable enable 

    public SunsetAPI(BotContext context, CommandManager cmdManager, PluginLoader loader, ILogger<SunsetAPI> logger)
    {
        BotContext = context;
        CommandManager = cmdManager;
        Logger = logger;
        PluginLoader = loader;
        BuildDatabase();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        CommandManager.RegisterCommand(Assembly.GetExecutingAssembly());
        PluginLoader.Load();
        SubscribeBotEvent();
        await BotContext.StarAsync(cancellationToken).ConfigureAwait(false);
    }

    public void BuildDatabase()
    {
        var dbName = "Sunset.sqlite";
        string sql = Path.Combine(PATH, dbName);
        if (Path.GetDirectoryName(sql) is string path)
        {
            Directory.CreateDirectory(path);
            DB = new SqliteConnection(string.Format("Data Source={0}", sql));
        }
    }

    public static void SubscribeBotEvent()
    {
        BotContext.Invoke.BotLogEvent += (bot, e) =>
        {
            Logger.Log(e.Level switch
            {
                Core.Enumerates.LogLevel.Debug => LogLevel.Debug,
                Core.Enumerates.LogLevel.Info => LogLevel.Information,
                Core.Enumerates.LogLevel.Warning => LogLevel.Warning,
                Core.Enumerates.LogLevel.Error => LogLevel.Error,
                Core.Enumerates.LogLevel.Critical => LogLevel.Critical,
                Core.Enumerates.LogLevel.Trace => LogLevel.Trace,
                _ => LogLevel.Information
            }, "{}", e.Message);
            return Task.CompletedTask;
        };

        BotContext.Invoke.OnGroupMessageReceived += CommandManager.MessageReceive;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        BotContext.Dispose();
        return Task.CompletedTask;
    }
}
