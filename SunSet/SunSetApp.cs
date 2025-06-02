using Microsoft.Extensions.Hosting;
using SunSet.Extensions;

namespace SunSet;

public sealed class SunSetApp
{
    private readonly HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder();

#nullable disable
    public static IServiceProvider ServiceProvider { get; private set; }
#nullable enable

    internal static SunSetApp Create() => new();

    public void Start()
    {
        var app = hostApplicationBuilder
            .AddSunSetServices()
            .Build();
        ServiceProvider = app.Services;
        app.Run();
    }
}
