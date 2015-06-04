namespace WSDL.Models.Binding
{
    public class RequestResponseOperation : Operation
    {
        public OperationMessage Input { get; set; }
        public OperationMessage Output { get; set; }
        public OperationFaultMessage Fault { get; set; }
    }
}