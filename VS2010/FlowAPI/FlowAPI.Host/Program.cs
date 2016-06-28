using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowAppInterface.Objects;

namespace FlowAppInterface.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new FlowConfigLogin(@"LAPTOP\FLOW2008R2", @"Scratch_Portal", string.Empty, string.Empty);
            bool loginresult = FlowAPI.DoLogin(config);
            Console.Write("Login Result: " + loginresult);
            Console.Read();
        }
    }
}
