/**
 * From Oreilly C#3.0 Design Patterns
 * The role of the Decorator pattern is to provide a way of attaching new state and behavior to an object dynamically.
 * The object does not know it is being "decorated", which makes this a useful pattern for evolving systems.
 * 
 * Uses:
 * Adding Text to pictures.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Decorator Pattern\n");

            IComponent component = new Component();
            Display("1. Basic Component: " , component);
            Display("2. A-Decorated Component: ", new DecoratorA(component));
            Display("3. B-Decorated Component: ", new DecoratorB(component));
            Display("3. B-A-Decorated Component: ", new DecoratorB(new DecoratorA(component)));

            //Explicit DecoratorB
            DecoratorB b = new DecoratorB(new Component());
            Display("5. A-B-Decorated: ", new DecoratorA(b));

            //Invoke added state
            Console.WriteLine("\t\t\t" + b.addedState + b.AddedBehavior());

        }

        static void Display(string s, IComponent c)
        {
            Console.WriteLine(s + c.Operation());
        }
    }

    interface IComponent
    {
        string Operation();
    }

    class Component : IComponent
    {

        public string Operation()
        {
            return "I am walking ";
        }
    }

    class DecoratorA : IComponent
    {
        IComponent component;
        public DecoratorA(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += "and listening to Classic FM ";
            return s;
        }
    }

    class DecoratorB : IComponent
    {
        IComponent component;
        public string addedState = "past the Coffee Shop ";

        public DecoratorB(IComponent c)
        {
            component = c;
        }

        public string Operation()
        {
            string s = component.Operation();
            s += "to school ";
            return s;
        }

        public string AddedBehavior()
        {
            return "and I bought a cappuccino ";
        }
    }
}
