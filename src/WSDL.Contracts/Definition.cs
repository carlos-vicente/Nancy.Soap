using System.Collections.Generic;
using System.Xml.Serialization;

namespace WSDL.Contracts
{
    [XmlType(Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class Definition
    {
        [XmlAttribute("targetNamespace")]
        public string TargetNamespace
        {
            get; // TODO: remote set and get obtains the contract namespace
            set;
        }

        [XmlElement("types")]
        public IEnumerable<Schema> Types { get; set; }

        [XmlElement("messages")]
        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<PortType> PortTypes { get; set; }

        public IEnumerable<Binding> Bindings { get; set; }

        public IEnumerable<Service> Services { get; set; }

        public Definition()
        {
            Types = new List<Schema>();
            Messages = new List<Message>();
            PortTypes = new List<PortType>();
            Bindings = new List<Binding>();
            Services = new List<Service>();
        }
    }
}
