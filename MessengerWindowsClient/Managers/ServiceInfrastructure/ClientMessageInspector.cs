using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using CommonLibrary;

namespace MessengerWindowsClient.Managers.ServiceInfrastructure
{
    public class ClientMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (ServiceManager.UniqueToken != null)
                request.Headers.Add(MessageHeader.CreateHeader("UniqueToken",
                    "http://zstumessenger.azurewebsites.com/messenger/uniqueToken/", ServiceManager.UniqueToken));

            //if (!request.Headers.Action.EndsWith("SetEncryptedSessionKey"))
            //{
            //    var iv = ServiceManager.Aes.GenerateNewIv();
            //    request.Headers.Add(MessageHeader.CreateHeader("IV",
            //        "http://zstumessenger.azurewebsites.com/messenger/IV/", iv));
            //    var bodyXmlReader = request.GetReaderAtBodyContents().ReadSubtree();
            //    var doc = new XmlDocument();
            //    doc.Load(bodyXmlReader);
            //    EncryptMessage(doc);
            //    var writer = new XmlBodyWriter(doc.InnerXml);
            //    var newMessage = Message.CreateMessage(request.Version, null, writer);
            //    newMessage.Properties.CopyProperties(request.Properties);
            //    newMessage.Headers.CopyHeadersFrom(request.Headers);
            //    request = newMessage;
            //}

            return null;
        }

        private void EncryptMessage(XmlDocument doc)
        {
            foreach (XmlNode childNode in doc.ChildNodes[0].ChildNodes)
            {
                childNode.InnerXml = ServiceManager.Aes.Encrypt(childNode.InnerXml);
                var text = childNode.InnerText;
                text = ServiceManager.Aes.Decrypt(text);
            }
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}
