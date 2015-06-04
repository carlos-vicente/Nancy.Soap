namespace WSDL.Models.Binding
{
    public class OperationMessage
    {
        public string Name { get; set; }

        public SoapExtensions.Body Body { get; set; }

        public SoapExtensions.Header Header { get; set; }
    }
}