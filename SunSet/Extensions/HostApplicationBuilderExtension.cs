using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SunSet.Commands;
using SunSet.Core;
using SunSet.Core.Enumerates;
using SunSet.Plugin;

namespace SunSet.Extensions;

public static class HostApplicationBuilderExtension
{
    public static HostApplicationBuilder AddSunSetServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<SunsetAPI>();
        builder.Services.AddSingleton(_ =>
        {
            return BotContext.CreateFactory(new ClientConfig()
            {
                AccessToken = builder.Configuration["BotClient:AccessToken"] ?? string.Empty,
                Host = builder.Configuration["BotClient:Host"] ?? "localhost",
                Port = int.TryParse(builder.Configuration["BotClient:Port"], out var port) ? port : 8080,
                ServiceType = Enum.TryParse<ServicesType>(builder.Configuration["BotClient:ServiceType"], true, out var serviceType) ? serviceType : throw new ArgumentException("Invalid ServiceType in configuration.")
            });
        });
        builder.Services.AddSingleton<CommandManager>();
        builder.Services.AddSingleton<PluginLoader>();
        return builder;
    }
}
