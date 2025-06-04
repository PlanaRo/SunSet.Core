namespace SunSet.Core.Network;

internal interface IServices
{
    event Action<string> OnMessageReceived;

    event Action OnServiceStarted;

    Task StartService(ClientConfig config, CancellationToken token);

    Task StopService();
}
