using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization
{
    public class Message : IXmlSerializable
    {
        public string Name { get; set; }

        public IEnumerable<MessagePart> Parts { get; set; }

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
            writer.WriteStartElement("message");

            writer.WriteAttributeString("name", Name);

            foreach (var messagePart in Parts)
            {
                messagePart.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}