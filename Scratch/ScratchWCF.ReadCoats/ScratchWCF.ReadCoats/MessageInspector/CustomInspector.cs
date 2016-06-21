using System;
using System.Xml;
using System.IO;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace ScratchWCF.ReadCoats.MessageInspector
{


    public class CustomInspector : IClientMessageInspector
    {

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            /*
            MemoryStream memStream = new MemoryStream();
            XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateBinaryWriter(memStream);
            xdw.WriteStartElement("GetDataResponse", "http://tempuri.org/");

            xdw.WriteStartElement("GetDataResult", "http://tempuri.org/");

            xdw.WriteAttributeString("Units", "ounces");

            xdw.WriteString("10.5");

            xdw.WriteEndElement();

            xdw.WriteEndElement();

            xdw.Flush();

            memStream.Position = 0;


            XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();

            XmlDictionaryReader xdr = XmlDictionaryReader.CreateBinaryReader(memStream, quotas);


            Message replacedMessage = Message.CreateMessage(reply.Version, null, xdr);

            replacedMessage.Headers.CopyHeadersFrom(reply.Headers);

            replacedMessage.Properties.CopyProperties(reply.Properties);

            reply = replacedMessage;
             * */

            var myReply = reply;

        }


        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)

        {
            var myRequest = request;

            return null;

        }



    }
}
