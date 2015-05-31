using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using WSDL.Models;
using WSDL.TypeManagement;

namespace WSDL.Tests
{
    public class GeneratorTests
    {
        private interface IContract
        {
            void OperationNoReturnNoParameters();
            string OperationWithReturnAndParameters(int p1, string p2);
        }

        private readonly Generator _sut;
        private readonly AutoFake _faker;

        public GeneratorTests()
        {
            _faker = new AutoFake();
            _sut = _faker.Resolve<Generator>();
        }

        public void GetWebServiceDefinition_ReturnsDefinition_WhenGettingSimpleTypeInterface()
        {
            // arrange
            const string defaultNamespace = "http://tempuri.org";

            //var intQName = new QName("int", "http://www.w3.org/2001/XMLSchema");
            //var stringQName = new QName("string", "http://www.w3.org/2001/XMLSchema");
            
            //A.CallTo(() => _faker.Resolve<IPrimitiveTypeProvider>()
            //    .GetPrimitiveTypesSchema())
            //    .Returns(primitiveTypesSchema);

            //A.CallTo(() => _faker.Resolve<IPrimitiveTypeProvider>()
            //    .GetQNameForType(typeof (Int32)))
            //    .Returns(intQName);

            //A.CallTo(() => _faker.Resolve<IPrimitiveTypeProvider>()
            //    .GetQNameForType(typeof(String)))
            //    .Returns(stringQName);

            var interfaceType = typeof (IContract);

            var method1 = interfaceType
                .GetMethods()
                .Single(m => m.Name.Equals("OperationNoReturnNoParameters"));

            var methodDescription1 = new MethodDescription
            {
                Input = new ComplexType("input1", new Sequence()),
                Output = new ComplexType("output1", new Sequence())
            };

            var method2 = interfaceType
                .GetMethods()
                .Single(m => m.Name.Equals("OperationWithReturnAndParameters"));

            var methodDescription2 = new MethodDescription
            {
                Input = new ComplexType("input2", new Sequence()),
                Output = new ComplexType("output2", new Sequence())
            };

            A.CallTo(() => _faker.Resolve<ITypeContextFactory>()
                .Create())
                .Returns(_faker.Resolve<ITypeContext>());

            A.CallTo(() => _faker.Resolve<ITypeContext>()
                .GetDescriptionForMethod(method1, defaultNamespace))
                .Returns(methodDescription1);

            A.CallTo(() => _faker.Resolve<ITypeContext>()
                .GetDescriptionForMethod(method2, defaultNamespace))
                .Returns(methodDescription2);

            var schemas = new List<Schema>
            {
                new Schema(),
                new Schema()
            };

            A.CallTo(() => _faker.Resolve<ITypeContext>()
                .GetSchemas())
                .Returns(schemas);

            var messages = new List<Message>
            {
                new Message
                {
                    Name = "IContract_OperationNoReturnNoParameters_InputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart(
                            "parameters", 
                            new QName(methodDescription1.Input.Name, defaultNamespace))
                    }
                },
                new Message
                {
                    Name = "IContract_OperationNoReturnNoParameters_OutputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart(
                            "parameters", 
                            new QName(methodDescription1.Output.Name, defaultNamespace))
                    }
                },
                new Message
                {
                    Name = "IContract_OperationWithReturnAndParameters_InputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart(
                            "parameters", 
                            new QName(methodDescription2.Input.Name, defaultNamespace))
                    }
                },
                new Message
                {
                    Name = "IContract_OperationWithReturnAndParameters_OutputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart(
                            "parameters", 
                            new QName(methodDescription2.Output.Name, defaultNamespace))
                    }
                }
            };

            var portType = new PortType
            {
                Name = "IContract",
                Operations = new List<Operation>
                {
                    new RequestResponseOperation
                    {
                        Name = "OperationNoReturnNoParameters",
                        Input = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationNoReturnNoParameters",
                            Message = new QName("IContract_OperationNoReturnNoParameters_InputMessage", defaultNamespace)
                        },
                        Output = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationNoReturnNoParametersResponse",
                            Message = new QName("IContract_OperationNoReturnNoParameters_OutputMessage", defaultNamespace)
                        }
                    },
                    new RequestResponseOperation
                    {
                        Name = "OperationWithReturnAndParameters",
                        Input = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationWithReturnAndParameters",
                            Message = new QName("IContract_OperationWithReturnAndParameters_InputMessage", defaultNamespace)
                        },
                        Output = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationWithReturnAndParametersResponse",
                            Message = new QName("IContract_OperationWithReturnAndParameters_OutputMessage", defaultNamespace)
                        }
                    }
                }
            };

            var expectedDefinition = new Definition
            {
                TargetNamespace = defaultNamespace,
                QualifiedNamespaces = new List<QNamespace>
                {
                    new QNamespace("tns", defaultNamespace)
                },
                Types = schemas,
                Messages = messages,
                PortTypes = new List<PortType> { portType },
                Bindings = new List<Binding>(),
                Services = new List<Service>()
            };

            // act
            var definition = _sut
                .GetWebServiceDefinition(interfaceType)
                .Result;

            // assert
            definition.ShouldBeEquivalentTo(expectedDefinition);
        }

        public void GetWebServiceDefinition_ThrowArgumentNullException_WhenContractIsNull()
        {
            // arrange
            // act
            Action act = () => _sut.GetWebServiceDefinition(null).Wait();

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
            Action act = () => _sut.GetWebServiceDefinition(typeof(Generator)).Wait();

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