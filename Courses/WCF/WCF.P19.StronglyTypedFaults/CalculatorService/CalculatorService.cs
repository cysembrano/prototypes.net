using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CalculatorService
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults=true)] //Another option to include fault exception details.
    public class CalculatorService : ICalculatorService
    {
        public int Divide(int Numerator, int Denominator)
        {
            if (Denominator == 0)
            {
                //throw new DivideByZeroException(); //This will throw an exception that will make your client's instance in a faulted state.  No recovering from this unless app restart.
                throw new FaultException("Divide by zero", new FaultCode("DivideByZeroFault")); // Throw a fault exception so as not to put he client's instance in faulted state.
            }

            return Numerator / Denominator;
        }
    }
}
