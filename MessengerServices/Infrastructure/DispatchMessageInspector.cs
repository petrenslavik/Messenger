using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using MessengerServices.Core;
using MessengerServices.Managers;

namespace MessengerServices.Infrastructure
{
    public class DispatchMessageInspector : IDispatchMessageInspector
    {
        private IAuthorizationManager _authorizationManager;

        public DispatchMessageInspector()
        {
            _authorizationManager = new AuthorizationManager();
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var headerIndex = request.Headers.FindHeader("UniqueToken",
                "http://zstumessenger.azurewebsites.com/messenger/uniqueToken/");

            if (headerIndex == -1)
            {
                return null;
            }

            var token = request.Headers.GetHeader<string>(headerIndex);

            var session = _authorizationManager.GetCurrentSession(token);

            OperationContext.Current.IncomingMessageProperties.Add("Session", session);

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}