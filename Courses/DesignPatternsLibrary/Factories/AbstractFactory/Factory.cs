using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factories.AbstractFactory
{
    public interface IAutoFactory
    {
        IAuto CreateAutomobile();
        IAuto CreateWagon();
        IAuto CreateSports();
    }







}
