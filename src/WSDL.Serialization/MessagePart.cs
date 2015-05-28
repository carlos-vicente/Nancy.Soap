using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization
{
    /// <summary>
    /// The base class for a message part, it can either be an ElementMessagePart or a TypeMessagePart
    /// </summary>
    public abstract class MessagePart : IXmlSerializable
    {
        public string Name { get; set; }
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public abstract void WriteXml(XmlWriter writer);
    }
}