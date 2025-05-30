using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using SunSet.Core.Milky;
using SunSet.Core.Network;
using SunSet.Core.Operation;

namespace SunSet.Core;

public class BotContext
{
    private readonly IServices _services;

    private readonly ClientConfig _config;

    private readonly OperationAdapter _adapter;

    public readonly Operation.EventHandler Invoke = new();

    public BotContext(ClientConfig config)
    {
        _services = new WebSocketServices();
        _config = config;
        _adapter = new OperationAdapter(this);
    }

    public async Task StarAsync()
    {
        _services.OnMessageReceived += async message =>
        {
            await _adapter.HandleOperationAsync(message);
        };

        await _services.StartService(_config);
    }

    public static BotContext CreateFactory(ClientConfig config) => new(config);
}
