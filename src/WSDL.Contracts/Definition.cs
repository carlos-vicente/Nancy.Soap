using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class Definition 
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
    }
}
