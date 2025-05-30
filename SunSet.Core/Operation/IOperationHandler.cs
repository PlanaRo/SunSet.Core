using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SunSet.Core.Operation;

internal interface IOperationHandler
{
    /// <summary>
    /// 处理操作
    /// </summary>
    /// <param name="operation">操作</param>
    /// <returns>是否处理成功</returns>
    Task HandleOperationAsync(BotContext bot, JsonNode node);

}
