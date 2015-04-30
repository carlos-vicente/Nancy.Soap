using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using SOAP.Dispatching;

namespace SoapExample
{
    using Nancy;
    using WSDL.Gen;

    public class Service : IService
    {
        public void DoSomething()
        {
            
        }

        public int AddTwoValues(int value1, int value2)
        {
            return value1 + value2;
        }
    }

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(
            TinyIoCContainer container, 
            IPipelines pipelines)
        {
            container.Register<IWsdlGenerator, WsdlGenerator>();
            container.Register<IDispatcher<IService>, Dispatcher<IService>>();
            container.Register<IService, Service>();
        }
    }
}