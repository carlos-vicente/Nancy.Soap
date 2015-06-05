using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Schema
{
    /// <summary>
    /// An base class that defines any type that can be used within a Schema.
    /// It can only be either a SimpleType or a ComplexType.
    /// </summary>
    public abstract class SchemaType : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader) { throw new System.NotImplementedException(); }

        public abstract void WriteXml(XmlWriter writer);
    }
}