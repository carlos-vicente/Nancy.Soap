using AutoMapper;
using Nancy;
using WSDL.Gen;
using Nancy.Bootstrapper;
using Nancy.SOAP.MappingProfiles;
using Nancy.TinyIoc;
using SOAP.Dispatching;

namespace SoapExample
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(
            TinyIoCContainer container, 
            IPipelines pipelines)
        {
            Mapper.Configuration.AddProfile<DefinitionMappingProfile>();
            container.Register(Mapper.Engine);

            container.Register<IPrimitiveTypeProvider, StaticPrimiteTypeProvider>();
            container.Register<IWsdlGenerator, WsdlGenerator>();

            container.Register<IDispatcher<IService>, Dispatcher<IService>>();
            container.Register<IService, Service>();
        }
    }
}