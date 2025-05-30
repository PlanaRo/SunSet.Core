
using SunSet.Core;

var bot = BotContext.CreateFactory(new()
{
    Host = "localhost",
    Port = 55119,
    AccessToken = "your_access_token_here",
    ServiceType = SunSet.Core.Enumerates.ServicesType.Websocket
});

bot.Invoke.OnGroupMessageReceived += async (sender, message, token) =>
{
    if(message.SenderUin == bot.BotUin)
    {
        Console.WriteLine("Received a group message with an invalid sender UIN.");
        return;
    }   
    Console.WriteLine($"Group Message from {message.GroupUin}: {message.Segments.Count}");
    message.Segments.GroupUin = message.GroupUin;
    await sender.Api.SendGroupMsg(message.Segments);
    // You can add more logic here to respond to the message
};

bot.StarAsync(new CancellationToken()).GetAwaiter().GetResult();
Console.ReadLine();
