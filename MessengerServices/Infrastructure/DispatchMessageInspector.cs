using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Xml;
using CommonLibrary;
using CommonLibrary.Security;
using MessengerServices.Core;
using MessengerServices.Managers;

namespace MessengerServices.Infrastructure
{
    public class DispatchMessageInspector : IDispatchMessageInspector
    {
        private IAuthorizationManager _authorizationManager;
        private Aes _aes;
        private IRequestHelper _requestHelper;

        public DispatchMessageInspector()
        {
            _requestHelper = new RequestHelper();
            _authorizationManager = new AuthorizationManager();
            _aes = new Aes();
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var tokenIndex = request.Headers.FindHeader("UniqueToken",
                "http://zstumessenger.azurewebsites.com/messenger/uniqueToken/");

            var ivIndex = request.Headers.FindHeader("IV", "http://zstumessenger.azurewebsites.com/messenger/IV/");

            if (tokenIndex != -1)
            {
                var token = request.Headers.GetHeader<string>(tokenIndex);

                var session = _authorizationManager.GetCurrentSession(token);

                OperationContext.Current.IncomingMessageProperties.Add("Session", session);
            }

            if (ivIndex != -1)
            {
                if (_requestHelper.GetCurrentSession() != null)
                {
                    _aes.SetAesKey(_requestHelper.GetCurrentSession().AesKey);
                }
                else
                {
                    if (MemoryCache.Default.Contains(_requestHelper.GetClientIp()))
                    {
                        _aes.SetAesKey(MemoryCache.Default.Get(_requestHelper.GetClientIp()) as byte[]);
                    }
                }

                var iv = request.Headers.GetHeader<byte[]>(ivIndex);
                _aes.SetAesIv(iv);
                //if (!request.Headers.Action.EndsWith("SetEncryptedSessionKey"))
                //{
                //    var doc = new XmlDocument();
                //    doc.LoadXml(request.ToString());
                //    var t = doc.GetElementsByTagName("s:Body");
                //    DecryptMessage(t[0], ref iv);
                //    var writer = new XmlBodyWriter(t[0].InnerXml);
                //    var newMessage = Message.CreateMessage(request.Version, null, writer);
                //    newMessage.Properties.CopyProperties(request.Properties);
                //    newMessage.Headers.CopyHeadersFrom(request.Headers);
                //    request = newMessage;
                //}
            }

            return null;
        }

        private void DecryptMessage(XmlNode doc, ref byte[] iv)
        {
            foreach (XmlNode childNode in doc.ChildNodes[0].ChildNodes)
            {
                childNode.InnerXml = _aes.Decrypt(childNode.InnerXml);
            }
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}