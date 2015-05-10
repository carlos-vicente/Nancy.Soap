using System.Xml;

namespace WSDL.Serialization
{
    public class TypeMessagePart : MessagePart
    {
        public QName Type { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            
        }
    }
}