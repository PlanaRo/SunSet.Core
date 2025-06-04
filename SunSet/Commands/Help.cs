using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text;

namespace SunSet.Commands;

public class Help : Command
{
    public override string[] Aliases { get; set; } = ["help", "h"];
    public override string[] Permissions { get; set; } = ["sunset.command.help"];
    public override string Description { get; set; } = "显示帮助信息，或查看指定命令的帮助信息。";

    public override async Task ExecuteAsync(CommandArgs args, ILogger logger)
    {
        if (args.Parameters.Count == 0)
        {
            var sb = new StringBuilder("可用命令列表:\n");
            foreach (var cmd in SunsetAPI.CommandManager.Command)
            {
                sb.AppendLine($"- {cmd.Aliases.FirstOrDefault()} : {cmd.Description}");
            }
            await args.Reply(sb.ToString().Trim());
            return;
        }
        
        var commandName = args.Parameters[0].ToLowerInvariant();
        var command = SunsetAPI.CommandManager.Command.FirstOrDefault(c => c.Aliases.Contains(commandName, StringComparer.OrdinalIgnoreCase));
        
        if (command == null)
        {
            await args.Reply($"未找到命令: {commandName}");
            return;
        }
        var cmdsubs = command.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(m => m.IsDefined(typeof(SubCommandAttribute)) && m.IsDefined(typeof(HelpTextAttribute)))
            .Select(m => (m.GetCustomAttribute<SubCommandAttribute>()!, m.GetCustomAttribute<HelpTextAttribute>()!))
            .ToList();
        var sbHelp = new StringBuilder($"命令: {command.Aliases.FirstOrDefault()}\n描述: {command.Description}\n权限: {string.Join(", ", command.Permissions)}\n子命令:\n");
        foreach (var cmd in cmdsubs) 
        {
            sbHelp.AppendLine($"- {cmd.Item1.Subname} : {cmd.Item2.Text}");
        }
        await args.Reply(sbHelp.ToString().Trim());
    }
}
