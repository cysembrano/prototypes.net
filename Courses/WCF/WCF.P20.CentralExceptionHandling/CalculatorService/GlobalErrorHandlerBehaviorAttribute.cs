using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace CalculatorService
{
    public class GlobalErrorHandlerBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;
        public GlobalErrorHandlerBehaviorAttribute(Type errorHandlerType)
        {
            this._errorHandlerType = errorHandlerType;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, 
                                          System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            IErrorHandler handler = (IErrorHandler)Activator.CreateInstance(this._errorHandlerType);

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(handler);
            }
        }


        public void AddBindingParameters(ServiceDescription serviceDescription, 
            System.ServiceModel.ServiceHostBase serviceHostBase, 
            System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, 
            System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }



        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }
    }
}
