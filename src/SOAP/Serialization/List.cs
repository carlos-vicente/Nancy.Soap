﻿using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    public class List : IXmlSerializable
    {
        public QName ItemType { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader) { throw new System.NotImplementedException(); }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("list", Schema.XmlSchemaNamespace);

            var prefix = writer.LookupPrefix(this.ItemType.Namespace);
            writer.WriteAttributeString("itemType", string.Format("{0}:{1}", prefix, this.ItemType.Name));
            
            writer.WriteEndElement();
        }
    }
}