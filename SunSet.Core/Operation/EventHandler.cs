
using System.Threading.Tasks;
using SunSet.Core.Milky;
using SunSet.Core.Milky.Message;

namespace SunSet.Core.Operation;

public class EventHandler
{
    public delegate Task OperationHandlerDelegate<T>(BotContext context, T args, CancellationToken token) where T : MilkyBaseData;

    public event OperationHandlerDelegate<MilkyGroupMessage>? OnGroupMessageReceived;

    public Task Call(BotContext context, MilkyBaseData args, CancellationToken token)
    {
        return args switch
        {
            MilkyGroupMessage msg => OnGroupMessageReceived?.Invoke(context, msg, token),
            _ => null
        } ?? Task.CompletedTask;
    }
}
