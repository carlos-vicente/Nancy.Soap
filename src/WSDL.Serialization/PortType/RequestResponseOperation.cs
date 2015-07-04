using System.Xml;

namespace WSDL.Serialization.PortType
{
    public class RequestResponseOperation : Operation
    {
        public OperationMessage Input { get; set; }
        public OperationMessage Output { get; set; }
        public OperationMessage Fault { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("operation");

            writer.WriteAttributeString("name", this.Name);

            // Input is mandatory
            Input.WriteXml(writer);

            // Output is mandatory
            Output.WriteXml(writer);

            // Fault is optional
            if (Fault != null)
                Fault.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}