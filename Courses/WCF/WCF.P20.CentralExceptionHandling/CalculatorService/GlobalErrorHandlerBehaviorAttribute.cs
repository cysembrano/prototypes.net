using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace CalculatorService
{
    public class GlobalErrorHandlerBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandler;
        public GlobalErrorHandlerBehaviorAttribute(Type errorHandlerType)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, 
                                          System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            throw new NotImplementedException();
        }


        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }



        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }
    }
}
