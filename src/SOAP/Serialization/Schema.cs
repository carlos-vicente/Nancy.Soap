using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class Schema : IXmlSerializable
    {
        public string TargetNamespace { get; set; }

        public IEnumerable<QNamespace> QualifiedNamespaces { get; set; }

        public IEnumerable<Element> Elements { get; set; }

        public IEnumerable<SchemaType> Types { get; set; }

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
