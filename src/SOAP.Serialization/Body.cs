using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class Body : IXmlSerializable
    {
        public string MethodName { get; private set; }

        public IDictionary<string, string> Parameters { get; private set; }

        public object Response { get; set; }

        public Body()
        {
            MethodName = string.Empty;
            Parameters = new Dictionary<string, string>();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Reads the XML content into the Parameters dictionary in order to be accessed and converted into the
        /// required types to invoke the target method
        /// </summary>
        /// <param name="reader">The input stream reader</param>
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(
                ElementNames.Body,
                Namespaces.SoapEnvelopeNamespace);

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

        private void RetrieveParameter(XmlReader reader)
        {
            Parameters.Add(
                reader.LocalName,
                reader.ReadInnerXml());
        }

        /// <summary>
        /// Writes the Response property onto the output stream as XML to be processed by the client
        /// </summary>
        /// <param name="writer">The output stream writer</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(
                ElementNames.Body,
                Namespaces.SoapEnvelopeNamespace);

            writer.WriteEndElement();
        }
    }
}