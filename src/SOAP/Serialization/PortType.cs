using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class PortType : IXmlSerializable
    {
        public string Name { get; set; }

        public IEnumerable<Operation> Operations { get; set; }

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