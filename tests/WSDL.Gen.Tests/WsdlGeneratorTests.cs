using System;
using FluentAssertions;

namespace WSDL.Gen.Tests
{
    public class WsdlGeneratorTests
    {
        public void GetWebServiceDefinition_ThrowArgumentNullException_WhenContractIsNull()
        {
            // arrange
            var generator = new WsdlGenerator();

            // act
            Action act = () => generator.GetWebServiceDefinition(null).Wait();

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
            var generator = new WsdlGenerator();

            // act
            Action act = () => generator.GetWebServiceDefinition(typeof(WsdlGenerator)).Wait();

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