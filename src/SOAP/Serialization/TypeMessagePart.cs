namespace SOAP.Serialization
{
    public class TypeMessagePart : MessagePart
    {
        public QName Type { get; set; }

        public TypeMessagePart(string name, QName type) 
            : base(name)
        {
            Type = type;
        }
    }
}