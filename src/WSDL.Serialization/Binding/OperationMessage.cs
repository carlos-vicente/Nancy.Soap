using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WSDL.Serialization.Binding.SoapExtensions;

namespace WSDL.Serialization.Binding
{
    public class OperationMessage : IXmlSerializable
    {
        public string ElementName { get; set; }

        public string Name { get; set; }

        public Body Body { get; set; }

        public Header Header { get; set; }
        
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
            writer.WriteStartElement(ElementName.ToLower());

            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                writer.WriteAttributeString("name", this.Name);
            }

            if(this.Body != null)
                this.Body.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}