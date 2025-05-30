using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Core.Network;

internal class ServicesMessage
{
    public string Message { get; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public DateTime Timestamp { get; set; }
    public ServicesMessage(string message, string sender, string receiver)
    {
        Message = message;
        Sender = sender;
        Receiver = receiver;
        Timestamp = DateTime.Now;
    }
}
