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
                new Element { Name = "anyType", Nillable = true, Type = new QName { Name ="anyType" }},
                new Element { Name = "anyURI", Nillable = true, Type = new QName { Name ="anyURI" }},
                new Element { Name = "base64Binary", Nillable = true, Type = new QName { Name ="base64Binary" }},
                new Element { Name = "boolean", Nillable = true, Type = new QName { Name ="boolean" }},
                new Element { Name = "byte", Nillable = true, Type = new QName { Name ="byte" }},
                new Element { Name = "dateTime", Nillable = true, Type = new QName { Name ="dateTime" }},
                new Element { Name = "decimal", Nillable = true, Type = new QName { Name ="decimal" }},
                new Element { Name = "double", Nillable = true, Type = new QName { Name ="double" }},
                new Element { Name = "float", Nillable = true, Type = new QName { Name ="float" }},
                new Element { Name = "int", Nillable = true, Type = new QName { Name ="int" }},
                new Element { Name = "long", Nillable = true, Type = new QName { Name ="long" }},
                new Element { Name = "Type", Nillable = true, Type = new QName { Name ="Type" }},
                new Element { Name = "short", Nillable = true, Type = new QName { Name ="short" }},
                new Element { Name = "string", Nillable = true, Type = new QName { Name ="string" }},
                new Element { Name = "unsignedByte", Nillable = true, Type = new QName { Name ="unsignedByte" }},
                new Element { Name = "unsignedInt", Nillable = true, Type = new QName { Name ="unsignedInt" }},
                new Element { Name = "unsignedLong", Nillable = true, Type = new QName { Name ="unsignedLong" }},
                new Element { Name = "unsignedShort", Nillable = true, Type = new QName { Name ="unsignedShort" }},
                new Element { Name = "char", Nillable = true, Type = new QName { Name ="char" }}
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
            var messageTypes = new Schema
            {
                Types = new List<SchemaType>()
                {
                    new ComplexType
                    {
                        
                    }
                }
            };

            var messages = new List<Message>
            {
                new Message
                {
                    Name = "OperationNoReturnNoParametersRequest",
                }
            };

            var expectedDefinition = new Definition
            {
                Types = new List<Schema>
                {
                    PrimitiveTypesSchema
                },
                Messages = new List<Message>(),
                PortTypes = new List<PortType>(),
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