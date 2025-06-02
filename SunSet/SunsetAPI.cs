using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SunSet.Core;

namespace SunSet;

internal class SunsetAPI(BotContext context, ILogger<SunsetAPI> logger) : IHostedService
{
    public static IServiceProvider ServiceProvider => SunSetApp.ServiceProvider;

    public static string PATH => Environment.CurrentDirectory;

    public static string PLUGIN_PATH => Path.Combine(PATH, "Plugins");

    public static string CONFIG_PATH => Path.Combine(PATH, "Config");

    public BotContext BotContext { get; } = context;

    public ILogger<SunsetAPI> Logger { get; } = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
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
        await BotContext.StarAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
