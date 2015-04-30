namespace WSDL.Contracts
{
    /// <summary>
    /// A message part that references an element that already exists in the description
    /// </summary>
    public class ElementMessagePart : MessagePart
    {
        public QName Element { get; set; }

        public ElementMessagePart(string name, QName element) 
            : base(name)
        {
            Element = element;
        }
    }
}