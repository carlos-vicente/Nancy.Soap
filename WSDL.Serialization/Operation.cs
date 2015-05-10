using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization
{
    /// <summary>
    /// WSDL has four operation types that an endpoint can support:
    /// - One-way. The endpoint receives a message. TODO
    /// - Request-response. The endpoint receives a message, and sends a correlated message.
    /// - Solicit-response. The endpoint sends a message, and receives a correlated message. TODO
    /// - Notification. The endpoint sends a message. TODO
    /// </summary>
    public abstract class Operation : IXmlSerializable
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