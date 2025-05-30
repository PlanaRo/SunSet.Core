
using SunSet.Core;

var bot = BotContext.CreateFactory(new()
{
    Host = "localhost",
    Port = 55119,
    AccessToken = "your_access_token_here",
    ServiceType = SunSet.Core.Enumerates.ServicesType.Websocket
});

bot.StarAsync().GetAwaiter().GetResult();
Console.ReadLine();
