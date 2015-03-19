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
            get; // TODO: remote set and have get obtain the contract namespace
            set;
        }

        [XmlElement("types")]
        public IEnumerable<Schema> Types { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<PortType> PortTypes { get; set; }

        public IEnumerable<Binding> Bindings { get; set; }

        public IEnumerable<Service> Services { get; set; }
    }
}
