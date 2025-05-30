using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using SunSet.Core.Milky;

namespace SunSet.Core.Operation;

internal class OperationAdapter
{
    private readonly Dictionary<string, IOperationHandler> _operationHandlers = [];

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
            .Where(t => t.IsDefined(typeof(MikyEventTypeAttribute)) && t.GetInterfaces().Contains(typeof(IOperationHandler))))
        {
            var attribute = type.GetCustomAttribute<MikyEventTypeAttribute>()!;
            
            if (Activator.CreateInstance(type) is IOperationHandler handler)
            {
                _operationHandlers[attribute.EventType] = handler;
            }
        }
    }

    public async Task HandleOperationAsync(string json)
    {
        var args = JsonSerializer.Deserialize<MilkyEventArgs>(json, jsonSerializerOptions)!;
        if (_operationHandlers.TryGetValue(args.EventType, out var handler))
        {
            await handler.HandleOperationAsync(_context, args.Payload);
        }
    }
}
