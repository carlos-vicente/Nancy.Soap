namespace SOAP.Serialization
{
    /// <summary>
    /// The base class for a message part, it can either be an ElementMessagePart or a TypeMessagePart
    /// </summary>
    public abstract class MessagePart
    {
        public string Name { get; set; }

        protected MessagePart(string name)
        {
            Name = name;
        }
    }
}