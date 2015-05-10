using System;
using System.Collections.Generic;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using WSDL.Models;

namespace WSDL.Tests
{
    public class GeneratorTests
    {
        private interface IContract
        {
            void OperationNoReturnNoParameters();
            string OperationWithReturnAndParameters(int p1, string p2);
        }

        private readonly Generator _generator;
        private readonly AutoFake _faker;

        public GeneratorTests()
        {
            _faker = new AutoFake();
            _generator = new Generator(_faker.Resolve<IPrimitiveTypeProvider>());
        }

        public void GetWebServiceDefinition_ReturnsDefinition_WhenGettingSimpleTypeInterface()
        {
            // arrange
            var primitiveTypesSchema = new Schema();
            var intQName = new QName("int", "tns");
            var stringQName = new QName("string", "tns");

            A.CallTo(() => _faker.Resolve<IPrimitiveTypeProvider>()
                .GetPrimitiveTypesSchema())
                .Returns(primitiveTypesSchema);

            A.CallTo(() => _faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof (Int32)))
                .Returns(intQName);

            A.CallTo(() => _faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(String)))
                .Returns(stringQName);

            var interfaceType = typeof (IContract);

            var messageTypes = new Schema
            {
                TargetNamespace = Generator.DefaultNamespace,
                QualifiedNamespaces = new List<QNamespace>
                {
                    new QNamespace("xs", "http://www.w3.org/2001/XMLSchema")
                },
                Types = new List<SchemaType>
                {
                    new ComplexType("OperationNoReturnNoParameters", new Sequence()),
                    new ComplexType("OperationNoReturnNoParametersResponse", new Sequence()),
                    new ComplexType("OperationWithReturnAndParameters", new Sequence
                    {
                        Elements = new List<Element>
                        {
                            new Element{ Name = "p1", Type = intQName},
                            new Element{ Name = "p2", Nillable = true, Type = stringQName}
                        }
                    }),
                    new ComplexType("OperationWithReturnAndParametersResponse", new Sequence
                    {
                        Elements = new List<Element>
                        {
                            new Element
                            { 
                                Name = "OperationWithReturnAndParametersResult",
                                Nillable = true,
                                Type = stringQName
                            }
                        }
                    })
                },
                Elements = new List<Element>
                {
                    new Element
                    {
                        Name = "OperationNoReturnNoParameters", 
                        Type = new QName("OperationNoReturnNoParameters", "tns")
                    },
                    new Element
                    {
                        Name = "OperationNoReturnNoParametersResponse", 
                        Type = new QName("OperationNoReturnNoParametersResponse", "tns")
                    },
                    new Element
                    {
                        Name = "OperationWithReturnAndParameters", 
                        Type = new QName("OperationWithReturnAndParameters", "tns")
                    },
                    new Element
                    {
                        Name = "OperationWithReturnAndParametersResponse", 
                        Type = new QName("OperationWithReturnAndParametersResponse", "tns")
                    }
                }
            };

            var messages = new List<Message>
            {
                new Message
                {
                    Name = "IContract_OperationNoReturnNoParameters_InputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName("OperationNoReturnNoParameters", "tns"))
                    }
                },
                new Message
                {
                    Name = "IContract_OperationNoReturnNoParameters_OutputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName("OperationNoReturnNoParametersResponse", "tns"))
                    }
                },
                new Message
                {
                    Name = "IContract_OperationWithReturnAndParameters_InputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName("OperationWithReturnAndParameters", "tns"))
                    }
                },
                new Message
                {
                    Name = "IContract_OperationWithReturnAndParameters_OutputMessage",
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName("OperationWithReturnAndParametersResponse", "tns"))
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
                            Message = new QName("IContract_OperationNoReturnNoParameters_InputMessage", "tns")
                        },
                        Output = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationNoReturnNoParametersResponse",
                            Message = new QName("IContract_OperationNoReturnNoParameters_OutputMessage", "tns")
                        }
                    },
                    new RequestResponseOperation
                    {
                        Name = "OperationWithReturnAndParameters",
                        Input = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationWithReturnAndParameters",
                            Message = new QName("IContract_OperationWithReturnAndParameters_InputMessage", "tns")
                        },
                        Output = new OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationWithReturnAndParametersResponse",
                            Message = new QName("IContract_OperationWithReturnAndParameters_OutputMessage", "tns")
                        }
                    }
                }
            };

            var expectedDefinition = new Definition
            {
                QualifiedNamespaces = new List<QNamespace>
                {
                    
                },
                Types = new List<Schema>
                {
                    primitiveTypesSchema,
                    messageTypes
                },
                Messages = messages,
                PortTypes = new List<PortType> { portType },
                Bindings = new List<Binding>(),
                Services = new List<Service>()
            };

            // act
            var definition = _generator
                .GetWebServiceDefinition(interfaceType)
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
            Action act = () => _generator.GetWebServiceDefinition(typeof(Generator)).Wait();

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