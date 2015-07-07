using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories.AbstractFactory
{
    public class HondaFactory : IAutoFactory
    {
        public IAuto CreateAutomobile()
        {
            var honda = new Honda();
            honda.Name = "Civic";

            return honda;
        }

        public IAuto CreateWagon()
        {
            var honda = new Honda();
            honda.Name = "Odyssey";

            return honda;
        }

        public IAuto CreateSports()
        {
            var honda = new Honda();
            honda.Name = "CR-Z";

            return honda;
        }
    }

    public class Honda : IAuto
    {
        public string Name { get; set; }

        public void TurnOn()
        {
            Console.WriteLine("Honda " + this.Name + " is running fast.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Honda " + this.Name + "  is off but you left the headlights on.");
        }
    }
}
