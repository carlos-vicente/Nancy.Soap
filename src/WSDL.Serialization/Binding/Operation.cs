using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Binding
{
    public abstract class Operation : IXmlSerializable
    {
        public string Name { get; set; }

        public SoapExtensions.Operation SoapOperation { get; set; }
        
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