using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factories.FactoryMethod
{
    public interface IAutoFactory
    {
        IAuto CreateAutomobile();
    }

    public class HondaFactory : IAutoFactory
    {
        public IAuto CreateAutomobile()
        {
            //Notice the construction of a Honda differs from that of Toyota
            var honda = new Honda();
            honda.Name = "Civic";

            return honda;
        }
    }

    public class ToyotaFactory : IAutoFactory
    {

        public IAuto CreateAutomobile()
        {
            //This one has parameters on constructor
            //And its name is just on a private field.
            return new Toyota("Corolla");
        }
    }

    public class MitsubishiFactory : IAutoFactory
    {
        //This one just doesn't have a Name property
        public IAuto CreateAutomobile()
        {
            return new Mitsubishi();
        }
    }

}
