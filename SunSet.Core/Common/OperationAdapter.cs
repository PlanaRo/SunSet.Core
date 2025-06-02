using System.Reflection;
using System.Text.Json;
using SunSet.Core.Milky;

namespace SunSet.Core.Common;

/// <summary>
/// Provides functionality to process and handle operations based on custom event types.
/// </summary>
/// <remarks>The <see cref="OperationAdapter"/> class is responsible for mapping event types to their
/// corresponding operation processors and executing the appropriate handler for a given operation. It uses reflection
/// to discover and register all types implementing <see cref="IOperationProcessor"/> that are annotated with the <see
/// cref="CustomEventAttribute"/>.</remarks>
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
        if (_operationHandlers.TryGetValue(args.EventType, out var handler))
        {
            await handler.HandleOperationAsync(_context, args.Payload, token);
        }
    }
}
