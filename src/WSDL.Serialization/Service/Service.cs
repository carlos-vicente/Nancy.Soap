using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Service
{
    public class Service : IXmlSerializable
    {
        public string Name { get; set; }

        public IEnumerable<Port> Ports { get; set; }

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
            writer.WriteStartElement("service");

            writer.WriteAttributeString("name", this.Name);

            foreach (var port in Ports)
            {
                port.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}