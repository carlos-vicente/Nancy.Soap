using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Schema
{
    // <simpleContent
    // id=ID
    // any attributes >
    // 
    // (annotation?,(restriction|extension))
    // 
    // </simpleContent>
    public class SimpleContent : IXmlSerializable
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