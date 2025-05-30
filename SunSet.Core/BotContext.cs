using SunSet.Core.Common;
using SunSet.Core.Network;

namespace SunSet.Core;

public class BotContext
{
    internal readonly IServices Services;

    internal readonly ClientConfig Config;

    private readonly OperationAdapter _adapter;

    public readonly Common.EventHandler Invoke = new();

    public readonly ApiRequestHandler Api;

    public uint BotUin { get; internal set; }

    public string BotName { get; internal set; } = string.Empty;

    public BotContext(ClientConfig config)
    {
        Services = config.ServiceType switch
        {
            Enumerates.ServicesType.Websocket => new WebSocketServices(),
            Enumerates.ServicesType.Webhook => new WebHookServices(),
            _ => throw new NotSupportedException($"Service type {config.ServiceType} is not supported.")
        };
        Config = config;
        Api = new ApiRequestHandler(this); 
        _adapter = new OperationAdapter(this);
    }

    public async Task StarAsync(CancellationToken token)
    {
        Services.OnMessageReceived += async message =>
        {
            await _adapter.HandleOperationAsync(message, token);
        };
        await Services.StartService(Config, token);
    }

    public static BotContext CreateFactory(ClientConfig config) => new(config);
}
