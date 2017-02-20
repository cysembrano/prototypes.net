using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CalculatorService
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults=true)] //Another option to include fault exception details.
    [GlobalErrorHandlerBehavior(typeof(GlobalErrorHandler))]
    public class CalculatorService : ICalculatorService
    {
        public int Divide(int Numerator, int Denominator)
        {
            //try
            //{
                return Numerator / Denominator;
            //}
            //catch (DivideByZeroException ex)
            //{
            //    DivideByZeroFault d = new DivideByZeroFault();
            //    d.Error = ex.Message;
            //    d.Details = "Denominator cannot be zero";

            //    throw new FaultException<DivideByZeroFault>(d);
            //}
        }
    }
}
