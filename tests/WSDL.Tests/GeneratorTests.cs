using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using WSDL.Models;
using WSDL.Models.Binding;
using WSDL.Models.Binding.SoapExtensions;
using WSDL.Models.Message;
using WSDL.Models.PortType;
using WSDL.Models.Schema;
using WSDL.Models.Service;
using WSDL.Models.Service.SoapExtensions;
using WSDL.TypeManagement;
using Binding = WSDL.Models.Binding.Binding;

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
            const string serviceEndpoint = "http://host/service";

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
                Operations = new List<Models.PortType.Operation>
                {
                    new Models.PortType.RequestResponseOperation
                    {
                        Name = "OperationNoReturnNoParameters",
                        Input = new Models.PortType.OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationNoReturnNoParameters",
                            Message = new QName("IContract_OperationNoReturnNoParameters_InputMessage", defaultNamespace)
                        },
                        Output = new Models.PortType.OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationNoReturnNoParametersResponse",
                            Message = new QName("IContract_OperationNoReturnNoParameters_OutputMessage", defaultNamespace)
                        }
                    },
                    new Models.PortType.RequestResponseOperation
                    {
                        Name = "OperationWithReturnAndParameters",
                        Input = new Models.PortType.OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationWithReturnAndParameters",
                            Message = new QName("IContract_OperationWithReturnAndParameters_InputMessage", defaultNamespace)
                        },
                        Output = new Models.PortType.OperationMessage
                        {
                            Action = "http://tempuri.org/IContract/OperationWithReturnAndParametersResponse",
                            Message = new QName("IContract_OperationWithReturnAndParameters_OutputMessage", defaultNamespace)
                        }
                    }
                }
            };

            var binding = new Binding
            {
                Name = "BasicHttpBinding_IContract",
                Type = new QName("IContract", defaultNamespace),
                SoapBinding = new Models.Binding.SoapExtensions.Binding
                {
                    Transport = Transport.Http,
                    Style = Style.Document
                },
                Operations = new List<Models.Binding.Operation>
                {
                    new Models.Binding.RequestResponseOperation
                    {
                        Name = "OperationNoReturnNoParameters",
                        SoapOperation = new Models.Binding.SoapExtensions.Operation
                        {
                            SoapAction = "http://tempuri.org/IContract/OperationNoReturnNoParameters"
                        },
                        Input = new Models.Binding.OperationMessage
                        {
                            Body = new Body
                            {
                                Use = OperationMessageUse.Literal
                            }
                        },
                        Output = new Models.Binding.OperationMessage
                        {
                            Body = new Body
                            {
                                Use = OperationMessageUse.Literal
                            }
                        }
                    },
                    
                    new Models.Binding.RequestResponseOperation
                    {
                        Name = "OperationWithReturnAndParameters",
                        SoapOperation = new Models.Binding.SoapExtensions.Operation
                        {
                            SoapAction = "http://tempuri.org/IContract/OperationWithReturnAndParameters"
                        },
                        Input = new Models.Binding.OperationMessage
                        {
                            Body = new Body
                            {
                                Use = OperationMessageUse.Literal
                            }
                        },
                        Output = new Models.Binding.OperationMessage
                        {
                            Body = new Body
                            {
                                Use = OperationMessageUse.Literal
                            }
                        }
                    }
                }
            };

            var service = new Service
            {
                Name = "ContractService",
                Ports = new List<Port>
                {
                    new Port
                    {
                        Name = binding.Name,
                        Binding = new QName(binding.Name, defaultNamespace),
                        Address = new Address
                        {
                            Location = serviceEndpoint
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
                Bindings = new List<Binding> { binding },
                Services = new List<Service> { service }
            };

            // act
            var definition = _sut
                .GetWebServiceDefinition(interfaceType, serviceEndpoint)
                .Result;

            // assert
            definition.ShouldBeEquivalentTo(expectedDefinition);
        }

        public void GetWebServiceDefinition_ReturnsDefinitionWithCorrectNamespace_WhenGettingSimpleTypeInterfaceForDifferentNamespace()
        {
            // arrange
            const string ns = "http://something.different.org/new";
            const string serviceEndpoint = "http://host/service";

            var interfaceType = typeof(IContract);

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
                .GetDescriptionForMethod(method1, ns))
                .Returns(methodDescription1);

            A.CallTo(() => _faker.Resolve<ITypeContext>()
                .GetDescriptionForMethod(method2, ns))
                .Returns(methodDescription2);

            var schemas = new List<Schema>
            {
                new Schema(),
                new Schema()
            };

            A.CallTo(() => _faker.Resolve<ITypeContext>()
                .GetSchemas())
                .Returns(schemas);
            
            // act
            var definition = _sut
                .GetWebServiceDefinition(interfaceType, serviceEndpoint, ns)
                .Result;

            // assert
            definition.TargetNamespace.ShouldBeEquivalentTo(ns);
        }

        public void GetWebServiceDefinition_ThrowArgumentNullException_WhenContractIsNull()
        {
            // arrange
            // act
            Action act = () => _sut.GetWebServiceDefinition(null, "endpoint").Wait();

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
            Action act = () => _sut.GetWebServiceDefinition(typeof(Generator), "endpoint").Wait();

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