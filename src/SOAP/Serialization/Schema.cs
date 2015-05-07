using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class Schema : IXmlSerializable
    {
        public const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

        public string TargetNamespace { get; set; }

        public IEnumerable<QNamespace> QualifiedNamespaces { get; set; }

        public IEnumerable<SchemaType> Types { get; set; }

        public IEnumerable<Element> Elements { get; set; }

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
            writer.WriteStartElement("xs", "schema", XmlSchemaNamespace);

            writer.WriteAttributeString("targetNamespace", this.TargetNamespace);

            foreach (var qualifiedNamespace in QualifiedNamespaces)
            {
                writer.WriteAttributeString(
                    "xmlns",
                    qualifiedNamespace.Abbreviation,
                    null,
                    qualifiedNamespace.Namespace);
            }

            WriteTypesXml(writer);
            WriteElementsXml(writer);

            writer.WriteEndElement();
        }

        private void WriteElementsXml(XmlWriter writer)
        {
            foreach (var type in Types)
            {
                type.WriteXml(writer);
            }
        }

        private void WriteTypesXml(XmlWriter writer)
        {
            
        }
    }
}
