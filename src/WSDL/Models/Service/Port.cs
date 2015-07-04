namespace WSDL.Models.Service
{
    public class Port
    {
        public string Name { get; set; }

        public QName Binding { get; set; }

        public SoapExtensions.Address Address { get; set; }
    }
}