using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SunSet.Core;
using SunSet.Core.Operation.Message;
using SunSet.Core.Segments;
using SunSet.Core.Segments.Entity;
using SunSet.Database.Managers;
using SunSet.Events;
using SunSet.Extensions;
using System.Reflection;
using System.Text;

namespace SunSet.Commands;

public class CommandManager(IConfiguration config, ILogger<CommandManager> logger)
{
    internal List<Command> Command { get; } = [];

    private ILogger<CommandManager> _logger { get; } = logger;

    private IConfiguration _config { get; } = config;

    private static List<string> ParseParameters(string str)
    {
        List<string> ret = [];
        var sb = new StringBuilder();
        bool instr = false;
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            if (c == '\\' && ++i < str.Length)
            {
                if (str[i] != '"' && str[i] != ' ' && str[i] != '\\')
                    sb.Append('\\');
                sb.Append(str[i]);
            }
            else if (c == '"')
            {
                instr = !instr;
                if (!instr)
                {
                    ret.Add(sb.ToString());
                    sb.Clear();
                }
                else if (sb.Length > 0)
                {
                    ret.Add(sb.ToString());
                    sb.Clear();
                }
            }
            else if (IsWhiteSpace(c) && !instr)
            {
                if (sb.Length > 0)
                {
                    ret.Add(sb.ToString());
                    sb.Clear();
                }
            }
            else
                sb.Append(c);
        }
        if (sb.Length > 0)
            ret.Add(sb.ToString());

        return ret;
    }

    private static bool IsWhiteSpace(char c)
    {
        return c == ' ' || c == '\t' || c == '\n';
    }

    internal async Task MessageReceive(BotContext bot, MilkyGroupMessage msg)
    { 
        var prefix = _config["BotSettings:CommandPrefix"] ?? string.Empty;
        var text = msg.Segments.GetEntities<TextEntity>().JoinToString(x => x.Text, " ");
        if (string.IsNullOrWhiteSpace(text) || !text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        text = text[prefix.Length..].TrimStart();
        var commandParts = ParseParameters(text);
        var commandName = commandParts[0].ToLowerInvariant();
        var cmd = Command.FirstOrDefault(c => c.Aliases.Contains(commandName, StringComparer.OrdinalIgnoreCase));
        if (cmd == null)
        {
            _logger.LogWarning("Command '{CommandName}' not found.", commandName);
            return;
        }
        var log = SunsetAPI.ServiceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(cmd.GetType())) as ILogger ?? _logger;
        var account = Account.GetAccount(msg.SenderUin);
        if (!cmd.Permissions.Any(account.HasPermission))
        {
            await bot.Action.SendGroupMsg(MessageChain.Group(msg.PeerUin).Text($"你没有权限执行命令 '{commandName}'。").Mention(msg.SenderUin));
            log.LogWarning("User {User} does not have permission to execute command '{CommandName}'.", msg.SenderUin, commandName);
            return;
        }
        var commandArgs = new CommandArgs(bot, msg, account, commandName, [.. commandParts.Skip(1)]);
        if(await OperateHandler.CommandEvent(commandArgs) == false)
        {
            log.LogInformation("Executing command '{CommandName}' for user {User}.", commandName, msg.SenderUin);
            try
            {
                await cmd.ExecuteAsync(commandArgs, log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                log.LogError(ex, "Error executing command '{CommandName}' for user {User}.", commandName, msg.SenderUin);
            }
        }
        else
        {
            log.LogWarning("Command '{CommandName}' execution was denied for user {User}.", commandName, msg.SenderUin);
        }
    }

    public void RegisterCommand(Assembly assembly)
    {
        var commandTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Command)) && !t.IsAbstract)
            .ToList();
        foreach (var type in commandTypes)
        {
            var command = Activator.CreateInstance(type) as Command;
            if (command != null)
            {
                Command.Add(command);
                _logger.LogInformation("Registered command: {CommandName}", command.Aliases.First());
            }
            else
            {
                _logger.LogWarning("Failed to create instance of command: {CommandName}", type.Name);
            }
        }
    }
}
