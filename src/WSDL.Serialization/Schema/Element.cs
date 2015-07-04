using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Schema
{
    public class Element : IXmlSerializable
    {
        public string Name { get; set; }

        public bool Nillable { get; set; }

        public QName Type { get; set; }

        public int? MinimumOccurrences { get; set; }

        public int? MaximumOccurrences { get; set; }
        
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
            writer.WriteStartElement("element", Schema.XmlSchemaNamespace);

            writer.WriteAttributeString("name", this.Name);

            string prefix = null;
            if (this.Type.Namespace != null)
                prefix = writer.LookupPrefix(this.Type.Namespace);

            writer.WriteAttributeString(
                "type",
                prefix == null
                    ? this.Type.Name
                    : string.Format("{0}:{1}", prefix, this.Type.Name));

            if(Nillable)
                writer.WriteAttributeString(
                    "nillable",
                    this.Nillable.ToString().ToLower());

            if(MinimumOccurrences.HasValue)
                writer.WriteAttributeString(
                    "minOccurs", 
                    this.MinimumOccurrences.Value.ToString());

            if (MaximumOccurrences.HasValue)
            {
                var value = this.MaximumOccurrences.Value.ToString();

                if (this.MaximumOccurrences.Value == int.MaxValue)
                    value = "unbounded";

                writer.WriteAttributeString("maxOccurs", value);
            }

            writer.WriteEndElement();
        }
    }
}