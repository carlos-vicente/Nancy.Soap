namespace WSDL.Models.Binding
{
    public abstract class Operation
    {
        public string Name { get; set; }

        public SoapExtensions.Operation SoapOperation { get; set; }
    }
}