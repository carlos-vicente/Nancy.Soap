using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization
{
    // <complexContent
    // id=ID
    // mixed=true|false
    // any attributes >
    // 
    // (annotation?,(restriction|extension))
    // 
    // </complexContent>
    public class ComplexContent : IXmlSerializable
    {
        public Restriction Restriction { get; set; }
        public Extension Extension { get; set; }
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            
        }
    }
}