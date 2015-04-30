namespace SOAP.Serialization
{
    /// <summary>
    /// WSDL has four operation types that an endpoint can support:
    /// - One-way. The endpoint receives a message. TODO
    /// - Request-response. The endpoint receives a message, and sends a correlated message.
    /// - Solicit-response. The endpoint sends a message, and receives a correlated message. TODO
    /// - Notification. The endpoint sends a message. TODO
    /// </summary>
    public abstract class Operation
    {
        public string Name { get; set; }
    }
}