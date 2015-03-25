using WSDL.Gen;

namespace Nancy.SOAP
{
    public abstract class SoapNancyModule<T> 
        : NancyModule where T : class
    {
        private readonly T _instance;

        protected SoapNancyModule(
            T instance,
            IWsdlGenerator wsdlGenerator)
        {
            _instance = instance;
        }
    }
}
