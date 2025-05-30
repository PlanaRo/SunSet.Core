using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Core.Network;

internal interface IServices
{
    
    void StartService();

    void StopService();

    void SendJsonAsync(string json);
}
