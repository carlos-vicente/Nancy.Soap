namespace WSDL.Models
{
    public class RequestResponseOperation : Operation
    {
        public OperationMessage Input { get; set; }
        public OperationMessage Output { get; set; }
        public OperationMessage Fault { get; set; }
    }
}