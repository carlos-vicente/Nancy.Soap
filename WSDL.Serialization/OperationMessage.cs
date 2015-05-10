using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization
{
    public class OperationMessage : IXmlSerializable
    {
        public const string AddressingNamespace = "http://www.w3.org/2006/05/addressing/wsdl";

        public string DirectionType { get; set; }

        public string Action { get; set; }

        public QName Message { get; set; }
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(DirectionType.ToLower());

            writer.WriteAttributeString("Action", AddressingNamespace, Action);

            string prefix = null;
            if (Message.Namespace != null)
                prefix = writer.LookupPrefix(Message.Namespace);

            writer.WriteAttributeString(
                "message",
                prefix == null
                    ? Message.Name
                    : string.Format("{0}:{1}", prefix, Message.Name));

            writer.WriteEndElement();
        }
    }
}