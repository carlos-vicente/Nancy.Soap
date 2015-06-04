namespace WSDL.Models.PortType
{
    public class OperationMessage
    {
        public string DirectionType { get; set; }

        public string Action { get; set; }

        public QName Message { get; set; }
    }
}