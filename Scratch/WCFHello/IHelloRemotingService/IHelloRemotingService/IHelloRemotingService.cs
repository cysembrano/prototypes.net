using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHelloRemotingService
{
    public interface IHelloRemotingService
    {
        string GetMessage(string name);
    }
}
