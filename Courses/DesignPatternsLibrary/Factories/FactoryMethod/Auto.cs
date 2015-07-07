using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories.FactoryMethod
{
    #region IAuto Concretes
    public interface IAuto
    {
        void TurnOn();
        void TurnOff();
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
            Console.WriteLine("Toyota is on and running.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Toyota is off.");
        }
    }

    public class Honda : IAuto
    {
        public string Name { get; set; }

        public void TurnOn()
        {
            Console.WriteLine("Honda is running fast.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Honda is off but you left the headlights on.");
        }
    }

    public class Mitsubishi : IAuto
    {

        public void TurnOn()
        {
            Console.WriteLine("Mitsubishi is slow starting.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Mitsubishi battery is dead");
        }
    }

    public class NullCar : IAuto
    {

        public void TurnOn()
        {
            Console.WriteLine("You did not specify a listed car.");
        }

        public void TurnOff()
        {
            Console.WriteLine("You did not specify a listed car.");
        }
    }
    #endregion
}
