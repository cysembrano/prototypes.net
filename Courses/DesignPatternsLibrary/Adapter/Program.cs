using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Client
    {
        static void Main(string[] args)
        {
            Adaptee adaptee = new Adaptee();
            adaptee.SpecificMethod();

            ITarget adapter = new Adapter();
            adapter.Method();
        }
    }

    class Adaptee 
    {
        public void SpecificMethod() { }
    }

    interface ITarget
    {
        void Method();
    }

    class Adapter : Adaptee, ITarget
    {
        public void Method()
        {
            throw new NotImplementedException();
        }
    }



}
