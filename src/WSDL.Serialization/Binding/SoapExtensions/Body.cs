using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Binding.SoapExtensions
{
    public class Body : IXmlSerializable
    {
        public OperationMessageUse Use { get; set; }
        
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
            writer.WriteStartElement("body", Namespaces.SoapNamespace);

            writer.WriteAttributeString(
                "use", 
                this.Use.ToString().ToLower());

            writer.WriteEndElement();
        }
    }
}