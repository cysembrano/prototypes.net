using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factories.SimpleFactory
{
    #region IAuto Concretes
    interface IAuto
    {
        void TurnOn();
        void TurnOff();
    }

    class Toyota : IAuto
    {

        public void TurnOn()
        {
            Console.WriteLine("Toyota is on and running.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Toyota is off.");
        }
    }

    class Honda : IAuto
    {

        public void TurnOn()
        {
            Console.WriteLine("Honda is running fast.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Honda is off but you left the headlights on.");
        }
    }

    class Mitsubishi : IAuto
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

    class NullCar : IAuto
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

    #region Factory
    class AutoFactory
    {
        Dictionary<string, Type> Autos;
        public AutoFactory()
        {
            LoadTypesICanReturn();
        }

        private void LoadTypesICanReturn()
        {
            Autos = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(IAuto).ToString()) != null)
                {
                    Autos.Add(type.Name.ToLower(), type);
                }
            }
        }

        public IAuto CreateInstance(string carName)
        {
            Type t = GetTypeToCreate(carName);

            if (t == null)
                return new NullCar();

            return Activator.CreateInstance(t) as IAuto;
        }

        private Type GetTypeToCreate(string carName)
        {
            foreach (var auto in Autos)
            {
                if (auto.Key.Contains(carName))
                {
                    return Autos[auto.Key];
                }
            }
            return null;
        }
    }

    #endregion
}
