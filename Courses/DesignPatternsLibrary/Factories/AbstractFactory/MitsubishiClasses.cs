using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories.AbstractFactory
{
    public class MitsubishiFactory : IAutoFactory
    {

        public IAuto CreateAutomobile()
        {
            return new Lancer();
        }


        public IAuto CreateWagon()
        {
            return new Chariot();
        }

        public IAuto CreateSports()
        {
            return new Evo();
        }
    }

    public abstract class Mitsubishi : IAuto
    {
        public virtual string Name { get; set; }

        public void TurnOn()
        {
            Console.WriteLine("Mitsubishi " + this.Name + " is slow starting.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Mitsubishi " + this.Name + " battery is dead");
        }
    }

    public class Lancer : Mitsubishi
    {
        public Lancer()
        {
            this.Name = "Lancer";
        }
    }

    public class Chariot : Mitsubishi
    {
        public Chariot()
        {
            this.Name = "Chariot";
        }
    }

    public class Evo : Mitsubishi
    {
        public Evo()
        {
            this.Name = "Evo";
        }
    }
}
