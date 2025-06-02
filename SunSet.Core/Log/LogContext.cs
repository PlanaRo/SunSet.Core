using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunSet.Core.Enumerates;

namespace SunSet.Core.Log;

public class LogContext(BotContext context)
{
    public void Log(string message, LogLevel level)=> context.Invoke.Call(context, new BotLogEventArgs(message, level));

    public void LogInformation(string message) => Log(message, LogLevel.Info);

    public void LogWarning(string message) => Log(message, LogLevel.Warning);

    public void LogError(string message) => Log(message, LogLevel.Error);

    public void LogCritical(string message) => Log(message, LogLevel.Critical);

    public void LogDebug(string message) => Log(message, LogLevel.Debug);
}
