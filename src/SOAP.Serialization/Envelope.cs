using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    [XmlRoot("Envelope", Namespace = Serialization.Namespaces.SoapEnvelopeNamespace)]
    public class Envelope : IXmlSerializable
    {
        public Header Header { get; private set; }

        public Body Body { get; private set; }

        public IDictionary<string, string> Namespaces { get; private set; }

        public Envelope()
        {
            Header = new Header();
            Body = new Body();
            Namespaces = new Dictionary<string, string>();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(
                ElementNames.Envelope,
                Serialization.Namespaces.SoapEnvelopeNamespace);

            Header.ReadXml(reader);
            Body.ReadXml(reader);

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(
                ElementNames.Envelope,
                Serialization.Namespaces.SoapEnvelopeNamespace);

            Header.WriteXml(writer);
            Body.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}
