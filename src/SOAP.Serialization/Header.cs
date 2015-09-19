using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class Header : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            //reader.ReadStartElement(ElementNames.Header, Namespaces.SoapEnvelopeNamespace);
            //reader.ReadEndElement();

            reader.ReadElementString(ElementNames.Header, Namespaces.SoapEnvelopeNamespace);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ElementNames.Header, Namespaces.SoapEnvelopeNamespace);
            writer.WriteEndElement();
        }
    }
}