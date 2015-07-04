using System.Xml;

namespace WSDL.Serialization.Binding
{
    public class RequestResponseOperation : Operation
    {
        public OperationMessage Input { get; set; }
        public OperationMessage Output { get; set; }
        public OperationFaultMessage Fault { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("operation");

            writer.WriteAttributeString("name", this.Name);

            if(this.SoapOperation != null)
                this.SoapOperation.WriteXml(writer);

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