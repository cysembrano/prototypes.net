using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace ScratchWCF.ReadCoats.MessageInspector
{

    public class CustomBehaviour : IEndpointBehavior
    {

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            //no-op
        }


        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            CustomInspector inspector = new CustomInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }


        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            //no-op
        }


        public void Validate(ServiceEndpoint endpoint)
        {
            //no-op
        }        

    }
}
