/*
 * FACTORY
 * FROM Wikipedia
 * In object-oriented programming, a factory is an object for creating other objects – 
 * formally a factory is simply an object that returns an object from some method call, 
 * which is assumed to be "new".[a] More broadly, a subroutine that returns a "new" 
 * object may be referred to as a "factory", as in factory method or factory function.
 * 
 * USES:
 * Unsure which concrete implementation
 * Creation should be separated from representation objec
 * Lots of if/else blocks or Switch to decide which concrete class
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Factories
{
    class Program
    {
        static void Main(string[] args)
        {
            //string carName = args[0];

            //Program.SimpleFactory(carName);

            //Program.FactoryMethod();

            Program.AbstractFactory();

            
        }

        #region SimpleFactory
        static void SimpleFactory(string carName)
        {
            Factories.SimpleFactory.AutoFactory factory = new Factories.SimpleFactory.AutoFactory();

            Factories.SimpleFactory.IAuto car = factory.CreateInstance(carName);

            car.TurnOn();
            car.TurnOff();
        }
        #endregion

        #region FactoryMethod
        static void FactoryMethod()
        {
            Factories.FactoryMethod.IAutoFactory autoFactory = LoadFactory();
            Factories.FactoryMethod.IAuto car = autoFactory.CreateAutomobile();

            car.TurnOn();
            car.TurnOff();
        }

        static Factories.FactoryMethod.IAutoFactory LoadFactory()
        {
            string factoryName = Properties.Settings.Default.AutoFactoryMethod;
            return Assembly.GetExecutingAssembly().CreateInstance(factoryName) as Factories.FactoryMethod.IAutoFactory;
        }

        #endregion

        #region AbstractFactory
        static Factories.AbstractFactory.IAutoFactory LoadAbstractFactory()
        {
            string factoryName = Properties.Settings.Default.AutoAbstractFactory;
            return Assembly.GetExecutingAssembly().CreateInstance(factoryName) as Factories.AbstractFactory.IAutoFactory;
        }

        static void AbstractFactory()
        {
            Factories.AbstractFactory.IAutoFactory factory = Program.LoadAbstractFactory();
            if (factory != null)
            {
                var car = factory.CreateAutomobile();
                car.TurnOn();
                car.TurnOff();

                car = factory.CreateSports();
                car.TurnOn();
                car.TurnOff();

                car = factory.CreateWagon();
                car.TurnOn();
                car.TurnOff();
            }
        }

        #endregion

    }

}
