using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization.Binding
{
    public class Binding : IXmlSerializable
    {
        public string Name { get; set; }

        public QName Type { get; set; }

        public SoapExtensions.Binding SoapBinding { get; set; }

        public IEnumerable<Operation> Operations { get; set; }

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
            writer.WriteStartElement("binding");

            writer.WriteAttributeString("name", Name);

            string prefix = null;
            if (Type.Namespace != null)
                prefix = writer.LookupPrefix(Type.Namespace);

            writer.WriteAttributeString(
                "type",
                prefix == null
                    ? Type.Name
                    : string.Format("{0}:{1}", prefix, Type.Name));

            if (SoapBinding != null)
                SoapBinding.WriteXml(writer);

            foreach (var operation in Operations)
            {
                operation.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}