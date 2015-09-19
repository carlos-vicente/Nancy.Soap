using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    [XmlRoot("Envelope", Namespace = Namespaces.SoapEnvelopeNamespace)]
    public class Envelope : IXmlSerializable
    {
        public Header Header { get; private set; }

        public Body Body { get; private set; }

        public Envelope()
        {
            Header = new Header();
            Body = new Body();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(
                ElementNames.Envelope, 
                Namespaces.SoapEnvelopeNamespace);

            Header.ReadXml(reader);
            Body.ReadXml(reader);

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
