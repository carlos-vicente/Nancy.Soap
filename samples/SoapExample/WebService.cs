using AutoMapper;
using Nancy.SOAP;
using SOAP.Dispatching;
using WSDL.Gen;

namespace SoapExample
{
    public class WebService : SoapNancyModule<IService>
    {
        public WebService(
            IService service, 
            IMappingEngine engine,
            IWsdlGenerator wsdlGenerator, 
            IDispatcher<IService> dispatcher) 
            : base("web", service, engine, wsdlGenerator, dispatcher)
        {
        }
    }
}