namespace WSDL.Models.Binding
{
    public class OperationFaultMessage
    {
        public string Name { get; set; }

        public SoapExtensions.Fault Fault { get; set; } 
    }
}