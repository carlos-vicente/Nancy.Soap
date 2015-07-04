using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Schema
{
    public abstract class ElementGrouping : IXmlSerializable
    {
        // Just a marker base class
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public abstract void WriteXml(XmlWriter writer);
    }
}