using SunSet.Core.Enumerates;

namespace SunSet.Core;

public class ClientConfig
{
    public string Host { get; init; } = "localhost";

    public int Port { get; init; } = 8080;

    public string AccessToken { get; init; } = string.Empty;

    public ServicesType ServiceType { get; init; } = ServicesType.Websocket;
}
