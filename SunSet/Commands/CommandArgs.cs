using SunSet.Core;
using SunSet.Core.Common.ApiResultArgs;
using SunSet.Core.Operation.Message;
using SunSet.Core.Segments;
using SunSet.Database.Managers;

namespace SunSet.Commands;

public class CommandArgs
{
    public BotContext Context { get; }

    public MilkyGroupMessage Message { get; }

    public Account Account { get; }

    public string CommandName { get;}

    public List<string> Parameters { get; }

    public bool Handler { get; set; }

    public CommandArgs(BotContext context, MilkyGroupMessage message, Account account, string cmdName, List<string> @params)
    {
        Context = context;
        Message = message;
        Account = account;
        CommandName = cmdName;
        Parameters = @params;
    }

    public async Task<GroupMessageResult> Reply(string text, bool mention = false)
    {
        Console.WriteLine(text);
        var msg = MessageChain.Group(Message.Group.GroupUin).Text(text);
        if (mention)
        {
            msg.Reply(Message.MessageSeq);
        }
        var result = await Context.Action.SendGroupMsg(msg);
        Console.WriteLine(result);
        return result.Data;
    }

}
