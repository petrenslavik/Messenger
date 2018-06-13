using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace MessengerWindowsClient.Managers.ServiceInfrastructure
{
    public class ClientMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (ServiceManager.UniqueToken != null)
                request.Headers.Add(MessageHeader.CreateHeader("UniqueToken",
                    "http://zstumessenger.azurewebsites.com/messenger/uniqueToken/", ServiceManager.UniqueToken));
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}
