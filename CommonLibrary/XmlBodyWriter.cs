using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonLibrary
{
    public class XmlBodyWriter:BodyWriter
    {
        private string body;

        public XmlBodyWriter(string strData):
            base(true)
        {
            body = strData;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            writer.WriteRaw(body);
        }
    }
}
