using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace CalculatorService
{
    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
                return;

            FaultException faultEx = new FaultException("A general service error occured");
            MessageFault messageFault = faultEx.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, null);
        }
    }
}
