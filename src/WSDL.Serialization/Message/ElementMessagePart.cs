using System.Xml;

namespace WSDL.Serialization.Message
{
    /// <summary>
    /// A message part that references an element that already exists in the description
    /// </summary>
    public class ElementMessagePart : MessagePart
    {
        public QName Element { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("part");

            writer.WriteAttributeString("name", Name);

            string prefix = null;
            if (this.Element.Namespace != null)
                prefix = writer.LookupPrefix(this.Element.Namespace);

            writer.WriteAttributeString(
                "element",
                prefix == null
                    ? this.Element.Name
                    : string.Format("{0}:{1}", prefix, this.Element.Name));

            writer.WriteEndElement();
        }
    }
}