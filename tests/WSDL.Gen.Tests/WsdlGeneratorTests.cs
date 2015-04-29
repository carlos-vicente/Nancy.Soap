using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using WSDL.Contracts;

namespace WSDL.Gen.Tests
{
    public class WsdlGeneratorTests
    {
        private interface IContract
        {
            void OperationNoReturnNoParameters();
            string OperationWithReturnAndParameters(int p1, string p2);
        }

        private static readonly Schema PrimitiveTypesSchema = new Schema
        {
            TargetNamespace = "http://schemas.microsoft.com/2003/10/Serialization/",
            Types = new List<SchemaType>
            {
                new SimpleType("char", new Restriction
                {
                    Base = "int"
                }),
                new SimpleType("duration", new Restriction
                {
                    Base = "duration",
                    Pattern = @"\-?P(\d*D)?(T(\d*H)?(\d*M)?(\d*(\.\d*)?S)?)?",
                    MinimumInclusive = "-P10675199DT2H48M5.4775808S",
                    MaximumInclusive = "P10675199DT2H48M5.4775807S"
                }),
                new SimpleType("guid", new Restriction
                {
                    Base = "string",
                    Pattern = @"[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12}"
                })
            },
            Elements = new List<Element>
            {
                new Element { Name = "anyType", Nillable = true, Type = new QName("anyType")},
                new Element { Name = "anyURI", Nillable = true, Type = new QName("anyURI")},
                new Element { Name = "base64Binary", Nillable = true, Type = new QName("base64Binary")},
                new Element { Name = "boolean", Nillable = true, Type = new QName("boolean")},
                new Element { Name = "byte", Nillable = true, Type = new QName("byte")},
                new Element { Name = "dateTime", Nillable = true, Type = new QName("dateTime" )},
                new Element { Name = "decimal", Nillable = true, Type = new QName("decimal" )},
                new Element { Name = "double", Nillable = true, Type = new QName("double" )},
                new Element { Name = "float", Nillable = true, Type = new QName("float" )},
                new Element { Name = "int", Nillable = true, Type = new QName("int" )},
                new Element { Name = "long", Nillable = true, Type = new QName("long" )},
                new Element { Name = "Type", Nillable = true, Type = new QName("Type" )},
                new Element { Name = "short", Nillable = true, Type = new QName("short" )},
                new Element { Name = "string", Nillable = true, Type = new QName("string" )},
                new Element { Name = "unsignedByte", Nillable = true, Type = new QName("unsignedByte" )},
                new Element { Name = "unsignedInt", Nillable = true, Type = new QName("unsignedInt" )},
                new Element { Name = "unsignedLong", Nillable = true, Type = new QName("unsignedLong" )},
                new Element { Name = "unsignedShort", Nillable = true, Type = new QName("unsignedShort" )},
                new Element { Name = "char", Nillable = true, Type = new QName("char" )}
            }
        };

        private readonly WsdlGenerator _generator;

        public WsdlGeneratorTests()
        {
            _generator = new WsdlGenerator();
        }

        public void GetWebServiceDefinition_ReturnsDefinition_WhenGettingSimpleTypeInterface()
        {
            // arrange

            // TODO: get this generation code into the actual SUT and get hardcoded values in the test

            var interfaceType = typeof (IContract);
            var types = new List<SchemaType>();
            var messageTypes = new Schema
            {
                TargetNamespace = Definition.DefaultNamespace,
                Types = types
            };

            var messages = new List<Message>();
            var operations = new List<Operation>();

            foreach (var method in interfaceType.Methods())
            {
                var inputParametersElements = new List<Element>();

                foreach (var parameter in method.GetParameters())
                {
                    var paramType = parameter.ParameterType.Name;
                    Element typeName = null; //

                    if (typeName != null)
                    {
                        inputParametersElements.Add(new Element
                        {
                            Name = parameter.Name,
                            Type = new QName(typeName.Name, "tns")
                        });
                    }
                }

                var inputType = new ComplexType(
                    string.Format("{0}", method.Name),
                    new Sequence {Elements = inputParametersElements});

                types.Add(inputType);

                var inputMessage = new Message
                {
                    Name = string.Format("{0}_InputMessage", method.Name),
                    Parts = new List<MessagePart>
                    {
                        new TypeMessagePart
                        {
                            Name = "parameters",
                            Type = new QName(inputType.Name, "tns")
                        }
                    }
                };

                var outputMessage = new Message
                {
                    Name = string.Format("{0}_OutputMessage", method.Name)
                };

                operations.Add(new RequestResponseOperation
                {
                    Name = method.Name,
                    Input = new OperationMessage
                    {
                        Name = string.Format("{0}Request", method.Name),
                        Message = new QName(inputMessage.Name, "tns")
                    },
                    Output = new OperationMessage
                    {
                        Name = string.Format("{0}Response", method.Name),
                        Message = new QName(outputMessage.Name, "tns")
                    }
                });

                messages.Add(inputMessage);
                messages.Add(outputMessage);
            }

            var portType = new PortType
            {
                Name = interfaceType.Name,
                Operations = operations
            };

            var expectedDefinition = new Definition
            {
                Types = new List<Schema>
                {
                    PrimitiveTypesSchema,
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