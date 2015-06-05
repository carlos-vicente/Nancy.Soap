using System.Xml;

namespace WSDL.Serialization.Message
{
    public class TypeMessagePart : MessagePart
    {
        public QName Type { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            
        }
    }
}