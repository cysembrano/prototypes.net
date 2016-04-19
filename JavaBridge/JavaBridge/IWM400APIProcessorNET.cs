using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace JavaBridge
{
    [Guid("03AE2678-E685-4050-B73C-019A17D5A22F")]
    [ComVisible(true)]
    public interface IWM4000APIProcessorNET
    {
        [DispId(1)]
        string Init();
        [DispId(2)]
        string Process(string callerIdentity, string typeofMessage, string action, string xmlFormattedMessage);


    }

    [Guid("CA3C3AD1-68DE-4BB0-83A8-CDB85272D01E"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface WM4000APIProcessorEvents { }
}
