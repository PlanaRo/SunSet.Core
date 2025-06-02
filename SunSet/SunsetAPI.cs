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
            Logger.LogInformation(e.Message);
            return Task.CompletedTask;
        };
        await BotContext.StarAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
