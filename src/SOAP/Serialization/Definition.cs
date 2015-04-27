using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization.Serialization
{
    [XmlRoot(
        "definitions",
        Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class Definition : IXmlSerializable
    {
        public string TargetNamespace { get; set; }

        public IEnumerable<Schema> Types { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<PortType> PortTypes { get; set; }

        public IEnumerable<Binding> Bindings { get; set; }

        public IEnumerable<Service> Services { get; set; }
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("targetNamespace", this.TargetNamespace);

            AddTypesElement(writer);
        }

        private void AddTypesElement(XmlWriter writer)
        {
            writer.WriteStartElement("types");

            foreach (var schema in Types)
            {
                schema.WriteXml(writer);
            }

            writer.WriteEndElement();

            foreach (var message in Messages)
            {
                message.WriteXml(writer);
            }

            foreach (var portType in PortTypes)
            {
                portType.WriteXml(writer);
            }

            foreach (var binding in Bindings)
            {
                binding.WriteXml(writer);
            }

            foreach (var service in Services)
            {
                service.WriteXml(writer);
            }
        }

        public void ReadXml(XmlReader reader) { throw new NotImplementedException(); }
    }
}