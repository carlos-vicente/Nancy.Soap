using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class Body : IXmlSerializable
    {
        // content: request or response
        public string MethodName { get; private set; }

        public IDictionary<string, string> Parameters { get; private set; }

        public Body()
        {
            MethodName = string.Empty;
            Parameters = new Dictionary<string, string>();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(ElementNames.Body, Namespaces.SoapEnvelopeNamespace);

            MethodName = reader.LocalName;

            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();

                while (reader.IsStartElement())
                {
                    RetrieveParameter(reader);
                }

                reader.ReadEndElement();
            }
            else
            {
                reader.ReadElementString();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        private void RetrieveParameter(XmlReader reader)
        {
            Parameters.Add(
                reader.LocalName,
                reader.ReadInnerXml());
        }
    }
}