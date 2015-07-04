using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Binding.SoapExtensions
{
    public class Binding : IXmlSerializable
    {
        private static readonly IDictionary<Transport, string> Transports = new Dictionary<Transport, string>
        {
            {Serialization.Binding.Transport.Http, "http://schemas.xmlsoap.org/soap/http"}
        };

        public Style? Style { get; set; }

        public Transport? Transport { get; set; }
        
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
            writer.WriteStartElement("binding", Namespaces.SoapNamespace);

            if (Transport.HasValue)
            {
                writer.WriteAttributeString(
                    "transport",
                    Transports[Transport.Value]); //throw error if unsupported transport is used
            }
                
            if(Style.HasValue)
            {
                writer.WriteAttributeString(
                    "style",
                    Style.Value.ToString());
            }

            writer.WriteEndElement();
        }
    }
}