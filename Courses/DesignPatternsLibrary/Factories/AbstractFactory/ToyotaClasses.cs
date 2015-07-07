using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories.AbstractFactory
{
    public class ToyotaFactory : IAutoFactory
    {

        public IAuto CreateAutomobile()
        {
            return new Toyota("Corolla");
        }

        public IAuto CreateWagon()
        {
            return new Toyota("Ipsum");
        }

        public IAuto CreateSports()
        {
            return new Toyota("Spyder");
        }
    }

    public class Toyota : IAuto
    {
        private readonly string Name;
        public Toyota(string name)
        {
            Name = name;
        }
        public void TurnOn()
        {
            Console.WriteLine("Toyota " + this.Name + " is on and running.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Toyota " + this.Name + " is off.");
        }
    }
}
