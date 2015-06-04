namespace WSDL.Models.Binding.SoapExtensions
{
    public class Operation
    {
        /// <summary>
        /// This URI value should be used directly as the value for the SOAPAction header
        /// For the HTTP protocol binding of SOAP, this is value required (it has no default value). 
        /// </summary>
        public string SoapAction { get; set; }

        /// <summary>
        /// If this is null, then the style defaults to the user defined at binding level
        /// </summary>
        public Style? Style { get; set; }
    }
}