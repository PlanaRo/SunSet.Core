using System.Reflection;
using System.Text.Json;
using SunSet.Core.Milky;

namespace SunSet.Core.Common;

internal class OperationAdapter
{
    private readonly Dictionary<string, IOperationProcessor> _operationHandlers = [];

    private readonly BotContext _context;

    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public OperationAdapter(BotContext context)
    {
        _context = context;
        foreach (var type in Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsDefined(typeof(CustomEventAttribute)) && t.GetInterfaces().Contains(typeof(IOperationProcessor))))
        {
            var attribute = type.GetCustomAttribute<CustomEventAttribute>()!;

            if (Activator.CreateInstance(type) is IOperationProcessor handler)
            {
                _operationHandlers[attribute.EventType] = handler;
            }
        }
    }

    public async Task HandleOperationAsync(string json, CancellationToken token)
    {
        var args = JsonSerializer.Deserialize<MilkyEventArgs>(json, jsonSerializerOptions)!;
        if (_context.BotUin < 10086)
        {
            var result = await _context.Api.GetLoginInfo();
            _context.BotName = result.Data.Nickname;
            _context.BotUin = result.Data.Uin;
        }
        if (_operationHandlers.TryGetValue(args.EventType, out var handler))
        {
            await handler.HandleOperationAsync(_context, args.Payload, token);
        }
    }
}
