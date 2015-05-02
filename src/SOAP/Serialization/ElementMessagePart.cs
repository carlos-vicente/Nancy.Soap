namespace SOAP.Serialization
{
    /// <summary>
    /// A message part that references an element that already exists in the description
    /// </summary>
    public class ElementMessagePart : MessagePart
    {
        public QName Element { get; set; }
    }
}