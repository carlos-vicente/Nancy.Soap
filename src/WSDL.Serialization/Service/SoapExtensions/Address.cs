using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Service.SoapExtensions
{
    public class Address : IXmlSerializable
    {
        public string Location { get; set; }
        
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
            writer.WriteStartElement("address", Namespaces.SoapNamespace);

            writer.WriteAttributeString("location", this.Location);
            
            writer.WriteEndElement();
        }
    }
}