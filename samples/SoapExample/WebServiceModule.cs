using Nancy.SOAP;

namespace SoapExample
{
    public class WebServiceModule : SoapNancyModule<IService>
    {
        public WebServiceModule(ISoapService<IService> soapService)
            : base("web", soapService)
        {
        }
    }
}