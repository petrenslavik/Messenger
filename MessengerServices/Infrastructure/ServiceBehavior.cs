using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace MessengerServices.Infrastructure
{
    public class ServiceBehavior : IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in chanDisp.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new DispatchMessageInspector());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}