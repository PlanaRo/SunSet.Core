using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunSet.Core.Network;

internal class WebHookServices : IServices
{
    public event Action<string>? OnMessageReceived;

    private readonly HttpListener _listener = new();

    public async Task StartService(ClientConfig config, CancellationToken token)
    {
        _listener.Prefixes.Add($"http://{config.Host}:{config.Port}/webhook/");
        _listener.Start();
        try
        {
            while (true)
            {
                await ReceiveLoopAsync(await _listener.GetContextAsync().WaitAsync(token), token);
            }

        }
        catch (TaskCanceledException)
        {
            //_logger.LogInformation("[HttpReceive] 监听停止: http://*:{Prot}/", _configuration["Server:Port"]);
        }
        catch (Exception ex)
        {
            //_logger.LogError("[HttpReceive] 监听异常: {ErrorMessage}", ex);
        }
    }

    private async Task ReceiveLoopAsync(HttpListenerContext httpListenerContext, CancellationToken cancellationToken)
    {
        var method = httpListenerContext.Request.HttpMethod;
        var body = await new StreamReader(httpListenerContext.Request.InputStream, httpListenerContext.Request.ContentEncoding).ReadToEndAsync(cancellationToken);
        if (method == "POST")
        {
            OnMessageReceived?.Invoke(body);
            httpListenerContext.Response.StatusCode = (int)HttpStatusCode.OK;
            await httpListenerContext.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("OK"), cancellationToken);
        }
        else
        {
            httpListenerContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            await httpListenerContext.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("Method Not Allowed"), cancellationToken);
        }
    }

    public Task StopService()
    {
        _listener.Stop();
        _listener.Close();
        return Task.CompletedTask;
    }
}
