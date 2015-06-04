using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using SOAP.Dispatching;
using WSDL;

namespace Nancy.SOAP.Tests
{
    public class SoapNancyModuleTests
    {
        public interface IContract
        {
            void OperationNoReturnNoParameters();
        }

        public class TestingModule : SoapNancyModule<IContract>
        {
            public TestingModule(
                IContract service, 
                IMappingEngine engine, 
                IGenerator wsdlGenerator, 
                IDispatcher<IContract> dispatcher) 
                : base("/", service, engine, wsdlGenerator, dispatcher)
            {
            }
        }

        private readonly AutoFake _faker;
        private readonly Browser _browser;

        public SoapNancyModuleTests()
        {
            _faker = new AutoFake();

            _browser = new Browser(config =>
            {
                config.Module<TestingModule>();
                
                var service = _faker.Resolve<IContract>();
                var mappingEngine = _faker.Resolve<IMappingEngine>();
                var generator = _faker.Resolve<IGenerator>();
                var dispatcher = _faker.Resolve<IDispatcher<IContract>>();
                
                config.Dependency(service);
                config.Dependency(mappingEngine);
                config.Dependency(generator);
                config.Dependency(dispatcher);
            });
        }

        public void GetWsdl_ObtainsWsdlForContract()
        {
            // arrange
            const string xmlContentType = "application/xml";
            var definitionToMap = new WSDL.Models.Definition();
            var mappedDefinition = new global::WSDL.Serialization.Definition();

            A.CallTo(() => _faker.Resolve<IGenerator>()
                .GetWebServiceDefinition(typeof (IContract), "TODO"))
                .Returns(Task.FromResult(definitionToMap));

            A.CallTo(() => _faker.Resolve<IMappingEngine>()
                .Map<WSDL.Models.Definition, global::WSDL.Serialization.Definition>(definitionToMap))
                .Returns(mappedDefinition);

            // act
            var response = _browser.Get("/wsdl", with =>
            {
                with.HttpRequest();
                with.Accept(new MediaRange(xmlContentType));
            });

            // assert
            response
                .ContentType
                .Should()
                .Be(xmlContentType);
        }
    }
}