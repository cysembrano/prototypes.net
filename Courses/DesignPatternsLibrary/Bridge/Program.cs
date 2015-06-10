/**
 * BRIDGE
 * From Wikipedia
 * The bridge pattern is a design pattern used in software engineering which is meant to "decouple an abstraction from its implementation so that the two can vary independently".[1] 
 * The bridge uses encapsulation, aggregation, and can use inheritance to separate responsibilities into different classes.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    class Client
    {
        static void Main(string[] args)
        {
            var someBridge1 = new SomeBridge(new Bridge1());
            someBridge1.CallMethod1();
            someBridge1.CallMethod2();

            var someBridge2 = new SomeBridge(new ReverseBridge());
            someBridge2.CallMethod1();
            someBridge2.CallMethod2();
        }
    }

    #region Implementation
    // Helps in providing truly decoupled architecture
    public interface IBridge
    {
        void Function1();
        void Function2();
    }
 
    public class Bridge1 : IBridge
    {
 
        #region IBridge Members
 
        public void Function1()
        {
            Console.WriteLine("Bridge1 Function 1.");
        }
 
        public void Function2()
        {
            Console.WriteLine("Bridge1 Function 2.");
        }
 
        #endregion
    }
 
    public class ReverseBridge : IBridge
    {
        #region IBridge Members
 
        public void Function1()
        {
            Console.WriteLine(string.Format(new string("Builder2 Function 1".Reverse().ToArray())));
        }
 
        public void Function2()
        {
            Console.WriteLine(string.Format(new string("Builder2 Function 2".Reverse().ToArray())));
        }
 
        #endregion
    }
    # endregion
 
    # region Abstraction
    public interface ISomeBridge
    {
        void CallMethod1();
        void CallMethod2();
    }

    public class SomeBridge : ISomeBridge 
    {
        public IBridge bridge;

        public SomeBridge(IBridge bridge)
        {
            this.bridge = bridge;
        }
        #region ISomeBridge Members

        public void CallMethod1()
        {
            this.bridge.Function1();
        }
 
        public void CallMethod2()
        {
            this.bridge.Function2();
        }
 
        #endregion
    }
    # endregion

}
