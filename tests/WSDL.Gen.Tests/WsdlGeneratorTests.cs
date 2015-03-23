using System;
using System.Collections.Generic;
using FluentAssertions;
using WSDL.Contracts;

namespace WSDL.Gen.Tests
{
    public class WsdlGeneratorTests
    {
        private interface IContract
        {
            void OperationNoReturnNoParameters();
            void OperationNoReturnWithParameters(int p1);
            string OperationWithReturnAndParameters(int p1, string p2);
        }

        private readonly WsdlGenerator _generator;

        public WsdlGeneratorTests()
        {
            _generator = new WsdlGenerator();
        }

        public void GetWebServiceDefinition_ReturnsDefinition_WhenGettingSimpleTypeInterface()
        {
            // arrange
            var expectedDefinition = new Definition
            {
                TargetNamespace = "namespace",
                Types = new List<Schema>
                {
                    new Schema(),   // request and responses schema
                    new Schema()    // primitive types schema
                },
                PortTypes = new List<PortType>(),
                Messages = new List<Message>(),
                Bindings = new List<Binding>(),
                Services = new List<Service>()
            };

            // act
            var definition = _generator
                .GetWebServiceDefinition(typeof(IContract))
                .Result;

            // assert
            definition.ShouldBeEquivalentTo(expectedDefinition);
        }

        public void GetWebServiceDefinition_ThrowArgumentNullException_WhenContractIsNull()
        {
            // arrange
            // act
            Action act = () => _generator.GetWebServiceDefinition(null).Wait();

            // assert
            act
                .ShouldThrow<ArgumentNullException>()
                .And
                .ParamName
                .Should()
                .Be("contract");
        }

        public void GetWebServiceDefinition_ThrowArgumentException_WhenContractIsNotAnInterface()
        {
            // arrange
            // act
            Action act = () => _generator.GetWebServiceDefinition(typeof(WsdlGenerator)).Wait();

            // assert
            act
                .ShouldThrow<ArgumentException>()
                .And
                .Message
                .Should()
                .Contain("An interface must be provided to generate a WSDL");
        }
    }
}