using Microsoft.Extensions.Logging;
using System.Reflection;

namespace SunSet.Commands;

public class Command
{
    public virtual string[] Aliases { get; set; } = [];

    public virtual string[] Permissions { get; set; } = [];

    public virtual string Description { get; set; } = string.Empty;

    public virtual string ErrorText { get; set; } = "命令错误，请使用正确的语法!";

    private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

    private readonly Dictionary<string, SubCommandExtra> SubCommands = [];

    public Command()
    {
        SubCommands = GetType()
            .GetMethods(bindingFlags)
            .Where(m => m.IsDefined(typeof(SubCommandAttribute), true))
            .Select(m =>
                new SubCommandExtra(m,
                    m.GetCustomAttribute<SubCommandAttribute>()!,
                    m.GetCustomAttribute<CommandPermissionAttribute>(),
                    m.GetCustomAttribute<HelpTextAttribute>()
                    )
                )
            .ToDictionary(s => s.SubCommand.Subname.ToLower());
    }

    public virtual async Task ExecuteAsync(CommandArgs args, ILogger logger)
    {
        if (args.Parameters.Count == 0)
        {
            await args.Reply(ErrorText);
            return;
        }
        var subcmd = args.Parameters[0].ToLower();
        if (!SubCommands.TryGetValue(subcmd, out var extar))
        {
            await args.Reply(ErrorText);
            return;
        }
        if (extar.CommandPermission != null && !extar.CommandPermission.DetectPermission(args.Account))
        {
            await args.Reply("你无权使用此命令!");
            return;
        }
        if (extar.SubCommand.Length > args.Parameters.Count)
        {
            await args.Reply(extar.HelpText != null ? $"语法错误，正确语法:{extar.HelpText.Text}" : ErrorText);
            return;
        }
        extar.Method.Invoke(null, [args, logger]);
    }

    public record SubCommandExtra(MethodInfo Method, SubCommandAttribute SubCommand, CommandPermissionAttribute? CommandPermission, HelpTextAttribute? HelpText);


}
