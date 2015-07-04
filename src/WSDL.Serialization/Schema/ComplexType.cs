using System.Xml;

namespace WSDL.Serialization.Schema
{
    public class ComplexType : SchemaType
    {
        public string Name { get; set; }

        public bool Abstract { get; set; }

        public SimpleContent SimpleContent { get; set; }

        public ComplexContent ComplexContent { get; set; }

        public ElementGrouping Grouping { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("complexType", Schema.XmlSchemaNamespace);

            writer.WriteAttributeString("name", Name);
            
            if (Abstract)
                writer.WriteAttributeString("abstract", Abstract.ToString().ToLower());

            if (SimpleContent != null) SimpleContent.WriteXml(writer);
            if (ComplexContent != null) ComplexContent.WriteXml(writer);
            if (Grouping != null) Grouping.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}