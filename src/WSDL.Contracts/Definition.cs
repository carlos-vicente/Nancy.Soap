using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Contracts
{
    [XmlRoot(
        "definitions", 
        Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class Definition : IXmlSerializable
    {
        public string TargetNamespace
        {
            get; // TODO: remote set and get obtains the contract namespace
            set;
        }

        public List<Schema> Types { get; set; }

        public List<Message> Messages { get; set; }

        public List<PortType> PortTypes { get; set; }

        public List<Binding> Bindings { get; set; }

        public List<Service> Services { get; set; }

        public Definition()
        {
            Types = new List<Schema>();
            Messages = new List<Message>();
            PortTypes = new List<PortType>();
            Bindings = new List<Binding>();
            Services = new List<Service>();
        }

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
            
            foreach (var type in Types)
            {
                writer.WriteStartElement("xsd", "schema", "http://www.w3.org/2000/10/XMLSchema");
                writer.WriteAttributeString("targetNamespace", type.TargetNamespace);

                // write types

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public void ReadXml(XmlReader reader) { throw new NotImplementedException(); }
    }
}
