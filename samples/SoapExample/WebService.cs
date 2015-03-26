using Nancy.SOAP;
using SOAP.Serialization.Dispatching;
using WSDL.Gen;

namespace SoapExample
{
    public class WebService : SoapNancyModule<IService>
    {
        public WebService(
            IService service, 
            IWsdlGenerator wsdlGenerator, 
            IDispatcher<IService> dispatcher) 
            : base("web", service, wsdlGenerator, dispatcher)
        {
        }
    }
}