﻿using AutoMapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.SOAP;
using Nancy.TinyIoc;
using SOAP.Dispatching;
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
            container.Register(Mapper.Engine);

            container.Register<ITypeContextFactory, TypeContextFactory>();
            container.Register<ITypeContext, TypeContext>();
            container.Register<IPrimitiveTypeProvider, StaticPrimitiveTypeProvider>();
            container.Register<ISoapService<IService>, SoapService<IService>>();
            container.Register<IGenerator, Generator>();

            container.Register<IDispatcher<IService>, Dispatcher<IService>>();
            container.Register<IService, Service>();
        }
    }
}