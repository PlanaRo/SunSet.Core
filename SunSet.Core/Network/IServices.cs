using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Core.Network;

internal interface IServices
{
    event Action<string> OnMessageReceived;

    Task StartService(ClientConfig config);

    Task StopService();

    Task SendJsonAsync(string json);
}
