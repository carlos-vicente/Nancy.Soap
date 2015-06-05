using AutoMapper;
using Nancy;
using Nancy.SOAP;
using SOAP.Dispatching;
using WSDL;

namespace SoapExample
{
    public class WebService : SoapNancyModule<IService>
    {
        public WebService(
            IService service, 
            IMappingEngine engine,
            IGenerator wsdlGenerator, 
            IDispatcher<IService> dispatcher)
            : base("web", service, engine, wsdlGenerator, dispatcher)
        {
        }
    }
}