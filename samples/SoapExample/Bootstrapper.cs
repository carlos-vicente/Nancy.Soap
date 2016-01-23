using AutoMapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using SOAP.Dispatching;
using SOAP.Serialization.MappingProfiles;
using SOAP.Service;
using WSDL;
using WSDL.Serialization.MappingProfiles;
using WSDL.TypeManagement;

namespace SoapExample
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(
            TinyIoCContainer container, 
            IPipelines pipelines)
        {
            Mapper.Configuration.AddProfile<DefinitionMappingProfile>();
            Mapper.Configuration.AddProfile<RequestMappingProfile>();

            Mapper.Configuration.Seal(); // this is REQUIRED in AutoMapper 4.0 to make inheritance work

            container.Register(Mapper.Engine);

            container.Register<ITypeContextFactory, TypeContextFactory>();
            container.Register<ITypeContext, TypeContext>();
            container.Register<IPrimitiveTypeProvider, StaticPrimitiveTypeProvider>();
            container.Register<ISoapService<IService>, SoapService<IService>>();
            container.Register<IGenerator, Generator>();

            container.Register<IDispatcher, Dispatcher<IService>>();
            container.Register<IService, Service>();
        }

        //protected override NancyInternalConfiguration InternalConfiguration
        //{
        //    get
        //    {
        //        return NancyInternalConfiguration
        //            .WithOverrides(
        //                cfg =>
        //                {
                            
        //                });
        //    }
        //}
    }
}