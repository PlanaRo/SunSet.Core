using SunSet.Core;

var bot = BotContext.CreateFactory(new()
{
    Host = "localhost",
    Port = 55119,
    AccessToken = "your_access_token_here",
    ServiceType = SunSet.Core.Enumerates.ServicesType.Websocket
});

bot.Invoke.BotLogEvent += async (sender, log) =>
{
    await Console.Out.WriteLineAsync($"[{log.Level}] {log.Message}");
};

bot.Invoke.OnGroupMessageReceived += async (sender, message) =>
{
    if(message.SenderUin == bot.BotUin || message.GroupUin != 1097364579)
    {
        //Console.WriteLine($"{message.Segments} from self, ignoring.");
        return;
    }
    //Console.WriteLine($"Group Message from {message.GroupUin}: {message.Segments}");
    message.Segments.GroupUin = message.GroupUin;
    await bot.Api.SendGroupMsg(message.Segments);
};

bot.StarAsync(new CancellationToken()).GetAwaiter().GetResult();
