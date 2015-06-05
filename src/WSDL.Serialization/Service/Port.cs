using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WSDL.Serialization.Service.SoapExtensions;

namespace WSDL.Serialization.Service
{
    public class Port : IXmlSerializable
    {
        public string Name { get; set; }

        public QName Binding { get; set; }

        public Address Address { get; set; }
        
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
            writer.WriteStartElement("port");

            writer.WriteAttributeString("name", this.Name);

            string prefix = null;
            if (Binding.Namespace != null)
                prefix = writer.LookupPrefix(Binding.Namespace);

            writer.WriteAttributeString(
                "binding",
                prefix == null
                    ? Binding.Name
                    : string.Format("{0}:{1}", prefix, Binding.Name));

            this.Address.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}