using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using MessengerServices.Core;

namespace MessengerServices.Managers
{
    public class RequestHelper:IRequestHelper
    {
        public string GetClientIp()
        {
            var operationContext = OperationContext.Current;
            var messageProperties = operationContext.IncomingMessageProperties;
            var remoteEndpointMessageProperty =
                (RemoteEndpointMessageProperty) messageProperties[RemoteEndpointMessageProperty.Name];
            return $"{remoteEndpointMessageProperty.Address}:{remoteEndpointMessageProperty.Port}";
        }
    }
}