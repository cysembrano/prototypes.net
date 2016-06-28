using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnmanagedDLL
{
    class MyDllClass
    {
        [DllExport]
        static string MyDllMethod()
        {
            return "You Got Me ";
        }
    }
}
