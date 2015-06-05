using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Binding.SoapExtensions
{
    public class Operation : IXmlSerializable
    {
        /// <summary>
        /// This URI value should be used directly as the value for the SOAPAction header
        /// For the HTTP protocol binding of SOAP, this is value required (it has no default value). 
        /// </summary>
        public string SoapAction { get; set; }

        /// <summary>
        /// If this is null, then the style defaults to the user defined at binding level
        /// </summary>
        public Style? Style { get; set; }

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
            writer.WriteStartElement("operation", Namespaces.SoapNamespace);

            writer.WriteAttributeString(
                    "soapAction",
                    SoapAction);

            if (Style.HasValue)
            {
                writer.WriteAttributeString(
                    "style",
                    Style.Value.ToString());
            }

            writer.WriteEndElement();
        }
    }
}