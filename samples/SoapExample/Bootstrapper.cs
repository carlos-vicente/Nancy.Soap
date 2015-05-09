using AutoMapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using SOAP.Dispatching;
using WSDL;
using WSDL.MappingProfiles;

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
            container.Register<IGenerator, Generator>();

            container.Register<IDispatcher<IService>, Dispatcher<IService>>();
            container.Register<IService, Service>();
        }
    }
}