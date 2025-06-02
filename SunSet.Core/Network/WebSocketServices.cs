using System;
using System.Net.WebSockets;
using System.Text;
using SunSet.Core.Log;

namespace SunSet.Core.Network;

// This class is a placeholder for WebSocket services.
internal class WebSocketServices(LogContext log) : IServices
{
    private readonly LogContext _log = log;

    private readonly ClientWebSocket _client = new();

    public event Action<string>? OnMessageReceived;
    public event Action? OnServiceStarted;

    public async Task StartService(ClientConfig config, CancellationToken token)
    {
        _client.Options.SetRequestHeader("Authorization", $"Bearer {config.AccessToken}");
        var uri = new Uri($"ws://{config.Host}:{config.Port}/event");
        await _client.ConnectAsync(uri, token);
        if (_client.State != WebSocketState.Open)
        {
            throw new InvalidOperationException("WebSocket connection failed.");
        }
        _log.LogInformation($"[{nameof(WebSocketServices)}]: Socket Connected To {uri}.");
        OnServiceStarted?.Invoke();
        await ReceiveLoopAsync(token);
    }

    private async Task ReceiveLoopAsync(CancellationToken token)
    {
        var buffer = new byte[1024];
        while (true)
        {
            int received = 0;
            while (true)
            {
                var result = await _client.ReceiveAsync(buffer.AsMemory(received), token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", token);
                    break;
                }

                received += result.Count;
                if (result.EndOfMessage) break;

                if (received == buffer.Length) Array.Resize(ref buffer, received << 1);
            }
            var text = Encoding.UTF8.GetString(buffer, 0, received);
            OnMessageReceived?.Invoke(text); // Handle user handlers error?
        }
    }

    public async Task StopService()
    {
        if (_client.State == WebSocketState.Open)
        {
            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Service stopped", default);
        }
        _client.Dispose();
        _log.LogInformation($"[{nameof(WebSocketServices)}]: Socket Close Connect.");
    }
}
