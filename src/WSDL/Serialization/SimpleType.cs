using System.Xml;

namespace WSDL.Serialization
{
    /// <summary>
    /// An xml simple type. 
    /// It can be a constraint on some other simple type (use property Restriction).
    /// It can be a list of some simple type (use property List).
    /// It can be the union of several simple types (use property Union).
    /// </summary>
    public class SimpleType : SchemaType
    {
        /// <summary>
        /// The simple type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Used when defining a constraint on some other simple type
        /// When using Restriction, can't use List nor Union
        /// </summary>
        public Restriction Restriction { get; set; }
        
        /// <summary>
        /// Used when defining a list of some simple type
        /// When using List, can't use Restriction nor Union
        /// </summary>
        public List List { get; set; }

        /// <summary>
        /// Used when defining the union of several simple types
        /// When using Union, can't use Restriction nor List
        /// </summary>
        public Union Union { get; set; }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("simpleType", Schema.XmlSchemaNamespace);

            writer.WriteAttributeString("name", this.Name);

            // only one of this xml elements will be written on to the document
            if (Restriction != null) Restriction.WriteXml(writer);
            if (List != null) List.WriteXml(writer);
            if (Union != null) Union.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}