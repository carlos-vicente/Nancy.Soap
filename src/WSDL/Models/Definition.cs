using System.Collections.Generic;

namespace WSDL.Models
{
    /// <summary>
    /// Holds the description of a web service. Web Services Description Language (WSDL)
    /// </summary>
    public class Definition
    {
        /// <summary>
        /// The target namespace, where the service exists and is unique.
        /// </summary>
        public string TargetNamespace
        {
            get; // TODO: remote set and get obtains the contract namespace
            set;
        }

        /// <summary>
        /// The namespaces and abbreviations for the types and elements used in the definition
        /// </summary>
        public IEnumerable<QNamespace> QualifiedNamespaces { get; set; }

        /// <summary>
        /// The types that can be found in the messages used by this web service.
        /// Defined as xml schemas.
        /// </summary>
        public IEnumerable<Schema> Types { get; set; }

        /// <summary>
        /// The input and output messages used by the web service to transport the data.
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }

        /// <summary>
        /// The interface description, basically the contracts that defines which operations exist
        /// and its input and output messages.
        /// </summary>
        public IEnumerable<PortType> PortTypes { get; set; }

        /// <summary>
        /// The binding between a PortType and the message format and protocol used for message exchanging.
        /// </summary>
        public IEnumerable<Binding> Bindings { get; set; }

        /// <summary>
        /// The service endpoints to call in order to invoke the web service here described.
        /// </summary>
        public IEnumerable<Service> Services { get; set; }

        /// <summary>
        /// Default constructor that initializes all properties to they're default values.
        /// </summary>
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
