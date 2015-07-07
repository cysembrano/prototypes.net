using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories.AbstractFactory
{
    #region IAuto Concretes
    public interface IAuto
    {
        void TurnOn();
        void TurnOff();
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
