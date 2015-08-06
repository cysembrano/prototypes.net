/*
 * * THIS IS WHAT YOU WILL SEE WHEN DISSASSEMBLED
 * // Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern Message
{
  .ver 1:0:0:0
}
 * **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;

namespace DotNetBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            var @class = new Class1();
            Console.Write(@class.GetMessage());
        }
    }
}
