using System.Net.WebSockets;
using System.Text;

namespace SunSet.Core.Network;

// This class is a placeholder for WebSocket services.
//正向Websocket服务类
internal class WebSocketServices : IServices
{
    private readonly ClientWebSocket _client = new();

    public event Action<string>? OnMessageReceived;


    public async Task StartService(ClientConfig config, CancellationToken token)
    {
        _client.Options.SetRequestHeader("Authorization", $"Bearer {config.AccessToken}");
        var uri = new Uri($"ws://{config.Host}:{config.Port}/event");
        await _client.ConnectAsync(uri, token);
        if (_client.State != WebSocketState.Open)
        {
            throw new InvalidOperationException("WebSocket connection failed.");
        }
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
    }
}
