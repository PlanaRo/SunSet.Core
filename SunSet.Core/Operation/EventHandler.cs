
using System.Threading.Tasks;
using SunSet.Core.Milky;
using SunSet.Core.Milky.Message;

namespace SunSet.Core.Operation;

public class EventHandler
{
    public delegate Task OperationHandlerDelegate<T>(T args) where T : MilkyBaseData;

    public event OperationHandlerDelegate<MilkyGroupMessage>? OnGroupMessageReceived;

    public async Task Call(MilkyBaseData args)
    {
        _ = args switch
        {
            MilkyGroupMessage msg => OnGroupMessageReceived?.Invoke(msg),
            _ => Task.CompletedTask
        };
    }
}
