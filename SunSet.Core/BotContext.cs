using SunSet.Core.Common;
using SunSet.Core.Log;
using SunSet.Core.Network;

namespace SunSet.Core;

public class BotContext
{
    internal readonly IServices Services;

    internal readonly ClientConfig Config;

    private readonly OperationAdapter _adapter;

    public readonly Common.EventHandler Invoke;

    public readonly ApiRequestHandler Action;

    public readonly LogContext Log;

    public uint BotUin { get; internal set; }

    public string BotName { get; internal set; } = string.Empty;

    public BotContext(ClientConfig config)
    {
        Config = config;
        Action = new ApiRequestHandler(this);
        _adapter = new OperationAdapter(this);
        Invoke = new Common.EventHandler(this);
        Log = new LogContext(this);
        Services = config.ServiceType switch
        {
            Enumerates.ServicesType.Websocket => new WebSocketServices(Log),
            Enumerates.ServicesType.Webhook => new WebHookServices(Log),
            _ => throw new NotSupportedException($"Service type {config.ServiceType} is not supported.")
        };
    }

    public async Task StarAsync(CancellationToken token)
    {
        Services.OnMessageReceived += async message =>
        {
            await _adapter.HandleOperationAsync(message, token);
        };
        Services.OnServiceStarted += async () =>
        {
            var result = await Action.GetLoginInfo();
            BotName = result.Data.Nickname;
            BotUin = result.Data.Uin;
            Log.LogInformation($"[{nameof(BotContext)}] Bot Name: {BotName}, Uin: {BotUin}");
        };
        await Services.StartService(Config, token);
    }

    public static BotContext CreateFactory(ClientConfig config) => new(config);
}
