﻿using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using Nancy.Testing;
using SOAP.Service;
using WSDL.Serialization;

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
            public TestingModule(ISoapService<IContract> service) 
                : base("/", service)
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

                var service = _faker.Resolve<ISoapService<IContract>>();
                
                config.Dependency(service);
            });
        }

        public void GetWsdl_ObtainsWsdlForContract()
        {
            // arrange
            const string xmlContentType = "application/xml";
            var definition = new Definition();

            A.CallTo(() => _faker.Resolve<ISoapService<IContract>>()
                .GetContractDefinition(A<string>._))
                .Returns(definition);

            // act
            var response = _browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // assert
            response
                .ContentType
                .Should()
                .Be(xmlContentType);

            A.CallTo(() => _faker.Resolve<ISoapService<IContract>>()
                .GetContractDefinition(A<string>._))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        public void InvokeOperation_InvokesSoapServiceWithEnvelopeAndSOAPAction()
        {
            
        }
    }
}