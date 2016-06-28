using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CyComObj
{
    [Guid("ED2B7E4E-A775-4543-860A-19B88BA9031C")]
    [ComVisible(true)]
    public interface IDBComObject
    {
        [DispId(1)]
        void Init();
        [DispId(2)]
        bool ExecuteSelectCommand(string selCommand);
        [DispId(3)]
        bool NextRow();
        [DispId(4)]
        void ExecuteNonSelectCommand(string insCommand);
        [DispId(5)]
        string GetColumnData(int pos);
    }

    [Guid("A920D25F-8302-4D30-9C35-7013E876D75C"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface DBComEvents { }
}
