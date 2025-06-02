using SunSet.Core.Enumerates;
using SunSet.Core.Milky;

namespace SunSet.Core.Log;

public class BotLogEventArgs(string message, LogLevel level) : MilkyBaseData
{
    public string Message { get; } = message;

    public LogLevel Level { get; } = level;
}
