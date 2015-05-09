using System.Collections.Generic;
using System.Xml;

namespace WSDL.Serialization
{
    public class Sequence : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("sequence", Schema.XmlSchemaNamespace);

            foreach (var element in Elements)
            {
                element.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}