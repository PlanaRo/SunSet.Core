﻿using SunSet.Core.Log;
using System.Net;
using System.Text;

namespace SunSet.Core.Network;

internal class WebHookServices(LogContext log) : IServices
{
    private readonly LogContext _log = log;

    public event Action<string>? OnMessageReceived;
    public event Action? OnServiceStarted;

    private readonly HttpListener _listener = new();

    public async Task StartService(ClientConfig config, CancellationToken token)
    {
        _listener.Prefixes.Add($"http://{config.Host}:{config.Port}/webhook/");
        _listener.Start();
        _log.LogInformation($"[{nameof(WebHookServices)}] Start Service: http://*:{config.Port}/webhook/");
        OnServiceStarted?.Invoke();
        try
        {
            while (true)
            {
                await ReceiveLoopAsync(await _listener.GetContextAsync().WaitAsync(token), token);
            }

        }
        catch (TaskCanceledException)
        {
            _log.LogInformation($"[{nameof(WebHookServices)}] Stop Services: http://*:{config.Port}/");
        }
        catch (Exception ex)
        {
            _log.LogError($"[{nameof(WebHookServices)}] Error Message: {ex}");
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
