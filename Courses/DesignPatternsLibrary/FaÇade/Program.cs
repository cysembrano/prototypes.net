/**
 * From wikipedia.
 * The Facade design pattern is often used when a system is very complex or difficult to understand because the system 
 * has a large number of interdependent classes or its source code is unavailable. This pattern hides the complexities 
 * of the larger system and provides a simpler interface to the client. It typically involves a single wrapper class 
 * which contains a set of members required by client. These members access the system on behalf of the facade client 
 * and hide the implementation details.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaÇade
{
    class Program
    {
        static void Main(string[] args)
        {
            Facade.Operation1();
            Facade.Operation2();
        }
    }

    #region Multiple Subsystems
    internal class SubSystemA
    {
        internal string A1() { return "Subsystem A, Method A1\n"; }
        internal string A2() { return "Subsystem A, Method A2\n"; }
    }

    internal class SubSystemB
    {
        internal string B1() { return "Subsystem B, Method B1\n"; }

    }
    internal class SubSystemC
    {
        internal string C1() { return "Subsystem C, Method C1\n"; }
        internal string C2() { return "Subsystem C, Method C2\n"; }
        internal string C3() { return "Subsystem C, Method C3\n"; }
    }
    #endregion

    #region FaÇade
    public static class Facade
    {
        static SubSystemA a = new SubSystemA();
        static SubSystemB b = new SubSystemB();
        static SubSystemC c = new SubSystemC();

        public static void Operation1()
        {
            Console.WriteLine("Operation 1\n" + a.A1() + a.A2() + b.B1());
        }
        public static void Operation2()
        {
            Console.WriteLine("Operation 2\n" + b.B1() + c.C1());
        }
    }
    #endregion

}
