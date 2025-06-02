using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SunSet.Core;
using SunSet.Core.Enumerates;

namespace SunSet.Extensions;

public static class HostApplicationBuilderExtension
{
    public static HostApplicationBuilder AddSunSetServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<SunsetAPI>();
        builder.Services.AddSingleton<BotContext>((s)=>
        { 
            return BotContext.CreateFactory(new ClientConfig()
            {
                AccessToken = builder.Configuration["BotClient:AccessToken"] ?? string.Empty,
                Host = builder.Configuration["BotClient:Host"] ?? "localhost",
                Port = int.TryParse(builder.Configuration["BotClient:Port"], out var port) ? port : 8080,
                ServiceType = Enum.TryParse<ServicesType>(builder.Configuration["BotClient:ServiceType"], true, out var serviceType) ? serviceType : throw new ArgumentException("Invalid ServiceType in configuration.")
            });
        });
        return builder;
    }
}
