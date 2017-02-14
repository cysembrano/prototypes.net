using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorService
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults=true)] //Another option to include fault exception details.
    public class CalculatorService : ICalculatorService
    {
        //If you put this on basicHttpBinding there are no sessions therefore faulting is ok on the client
        //But wsHttpBinding has sessions so faulted state instances of the client can no longer be used.
        public int Divide(int Numerator, int Denominator)
        {
            return Numerator / Denominator;
        }
    }
}
