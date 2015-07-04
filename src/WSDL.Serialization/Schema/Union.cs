using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Schema
{
    public class Union : IXmlSerializable
    {
        public string Id { get; set; }

        public IEnumerable<QName> MemberTypes { get; set; }
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader) { throw new NotImplementedException(); }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("union", Schema.XmlSchemaNamespace);

            var listOfTypes = string.Join(
                " ",
                MemberTypes
                    .Select(mt => 
                        mt.Namespace == null
                        ? mt.Name
                        : string.Format(
                            "{0}:{1}",
                            writer.LookupPrefix(mt.Namespace),
                            mt.Name)));

            writer.WriteAttributeString("memberTypes", listOfTypes);

            writer.WriteEndElement();
        }
    }
}