using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class Message : IXmlSerializable
    {
        public string Name { get; set; }

        public IEnumerable<MessagePart> Parts { get; set; }

        public Message()
        {
            Parts = new List<MessagePart>();
        }

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
            
        }
    }
}