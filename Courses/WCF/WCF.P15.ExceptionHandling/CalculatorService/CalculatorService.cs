using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorService
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults=true)] //Another option to include fault exception details.
    public class CalculatorService : ICalculatorService
    {
        public int Divide(int Numerator, int Denominator)
        {
            return Numerator / Denominator;
        }
    }
}
