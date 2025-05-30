using SunSet.Core.Network;
using SunSet.Core.Operation;

namespace SunSet.Core;

public class BotContext
{
    internal readonly IServices Services;

    internal readonly ClientConfig Config;

    private readonly OperationAdapter _adapter;

    public readonly Operation.EventHandler Invoke = new();

    public readonly ApiHandler Api;

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
        Api = new ApiHandler(this); 
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
