using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WSDL.Serialization.Binding.SoapExtensions;

namespace WSDL.Serialization.Binding
{
    public class OperationFaultMessage : IXmlSerializable
    {
        public string Name { get; set; }

        public Fault Fault { get; set; }
        
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
            throw new System.NotImplementedException();
        }
    }
}