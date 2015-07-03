using Autofac.Extras.FakeItEasy;
using System;
using System.Collections.Generic;
using WSDL.Tests.Fixie;
using WSDL.TypeManagement;
using FluentAssertions;
using WSDL.Models;
using WSDL.Models.Schema;

namespace WSDL.Tests
{
    public class StaticPrimitiveTypeProviderTests
    {
        private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";
        private const string SerializationNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";

        #region Simple Types
        private static readonly SimpleType CharType = new SimpleType("char", new Restriction
        {
            Base = new QName("int", XmlSchemaNamespace)
        });

        private static readonly SimpleType DurationType = new SimpleType("duration", new Restriction
        {
            Base = new QName("duration", XmlSchemaNamespace),
            Pattern = @"\-?P(\d*D)?(T(\d*H)?(\d*M)?(\d*(\.\d*)?S)?)?",
            MinimumInclusive = "-P10675199DT2H48M5.4775808S",
            MaximumInclusive = "P10675199DT2H48M5.4775807S"
        });

        private static readonly SimpleType GuidType = new SimpleType("guid", new Restriction
        {
            Base = new QName("string", XmlSchemaNamespace),
            Pattern = @"[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12}"
        });
        #endregion

        #region Elements
        private static readonly Element AnyType = new Element
        {
            Name = "anyType",
            Nillable = true,
            Type = new QName("anyType", XmlSchemaNamespace)
        };

        private static readonly Element AnyUri = new Element
        {
            Name = "anyURI",
            Nillable = true,
            Type = new QName("anyURI", XmlSchemaNamespace)
        };

        private static readonly Element Base64Binary = new Element
        {
            Name = "base64Binary",
            Nillable = true,
            Type = new QName("base64Binary", XmlSchemaNamespace)
        };

        private static readonly Element Boolean = new Element
        {
            Name = "boolean",
            Nillable = true,
            Type = new QName("boolean", XmlSchemaNamespace)
        };

        private static readonly Element Byte = new Element
        {
            Name = "byte",
            Nillable = true,
            Type = new QName("byte", XmlSchemaNamespace)
        };

        private static readonly Element DateTime = new Element
        {
            Name = "dateTime",
            Nillable = true,
            Type = new QName("dateTime", XmlSchemaNamespace)
        };

        private static readonly Element Decimal = new Element
        {
            Name = "decimal",
            Nillable = true,
            Type = new QName("decimal", XmlSchemaNamespace)
        };

        private static readonly Element Double = new Element
        {
            Name = "double",
            Nillable = true,
            Type = new QName("double", XmlSchemaNamespace)
        };

        private static readonly Element Float = new Element
        {
            Name = "float",
            Nillable = true,
            Type = new QName("float", XmlSchemaNamespace)
        };

        private static readonly Element Int = new Element
        {
            Name = "int",
            Nillable = true,
            Type = new QName("int", XmlSchemaNamespace)
        };

        private static readonly Element Long = new Element
        {
            Name = "long",
            Nillable = true,
            Type = new QName("long", XmlSchemaNamespace)
        };

        private static readonly Element QName = new Element
        {
            Name = "QName",
            Nillable = true,
            Type = new QName("QName", XmlSchemaNamespace)
        };

        private static readonly Element Short = new Element
        {
            Name = "short",
            Nillable = true,
            Type = new QName("short", XmlSchemaNamespace)
        };

        private static readonly Element String = new Element
        {
            Name = "string",
            Nillable = true,
            Type = new QName("string", XmlSchemaNamespace)
        };

        private static readonly Element UnsignedByte = new Element
        {
            Name = "unsignedByte",
            Nillable = true,
            Type = new QName("unsignedByte", XmlSchemaNamespace)
        };

        private static readonly Element UnsignedInt = new Element
        {
            Name = "unsignedInt",
            Nillable = true,
            Type = new QName("unsignedInt", XmlSchemaNamespace)
        };

        private static readonly Element UnsignedLong = new Element
        {
            Name = "unsignedLong",
            Nillable = true,
            Type = new QName("unsignedLong", XmlSchemaNamespace)
        };

        private static readonly Element UnsignedShort = new Element
        {
            Name = "unsignedShort",
            Nillable = true,
            Type = new QName("unsignedShort", XmlSchemaNamespace)
        };

        private static readonly Element Char = new Element
        {
            Name = "char",
            Nillable = true,
            Type = new QName("char", SerializationNamespace)
        };

        private static readonly Element Duration = new Element
        {
            Name = "duration",
            Nillable = true,
            Type = new QName("duration", SerializationNamespace)
        };

        private static readonly Element Guid = new Element
        {
            Name = "guid",
            Nillable = true,
            Type = new QName("guid", SerializationNamespace)
        };
        #endregion

        private readonly StaticPrimitiveTypeProvider _sut;
        
        public StaticPrimitiveTypeProviderTests()
        {
            var faker = new AutoFake();
            _sut = faker.Resolve<StaticPrimitiveTypeProvider>();
        }

        [Input(typeof(Int16))]
        [Input(typeof(Int32))]
        [Input(typeof(String))]
        [Input(typeof(Char))]
        [Input(typeof(Guid))]
        [Input(typeof(Char))]
        [Input(typeof(Boolean))]
        [Input(typeof(Byte))]
        [Input(typeof(DateTime))]
        [Input(typeof(Decimal))]
        [Input(typeof(Double))]
        [Input(typeof(float))]
        [Input(typeof(long))]
        [Input(typeof(UInt32))]
        [Input(typeof(UInt16))]
        [Input(typeof(UInt64))]
        public void IsPrimitive_ReturnsTrue_ForAllSoapPrimitiveType(Type primitiveType)
        {
            // arrange

            // act
            var actual = _sut.IsPrimitive(primitiveType);

            // assert
            actual.Should().BeTrue();
        }

        [Input(typeof(StaticPrimitiveTypeProvider))]
        [Input(typeof(IPrimitiveTypeProvider))]
        public void IsPrimitive_ReturnsFalse_ForTypesNotInSoapPrimitiveCollection(Type nonPrimitiveType)
        {
            // arrange

            // act
            var actual = _sut.IsPrimitive(nonPrimitiveType);

            // assert
            actual.Should().BeFalse();
        }

        private static readonly IDictionary<Type, QName> ExpectedQNames = new Dictionary<Type, QName>
        {
            {typeof (Int16), new QName("short", XmlSchemaNamespace)},
            {typeof (Int32), new QName("int", XmlSchemaNamespace)},
            {typeof (String), new QName("string", XmlSchemaNamespace)},
            {typeof (Char), new QName("char",SerializationNamespace)},
            {typeof (Guid), new QName("guid",SerializationNamespace)},
            {typeof (Boolean), new QName("boolean", XmlSchemaNamespace)},
            {typeof (Byte), new QName("byte", XmlSchemaNamespace)},
            {typeof (DateTime), new QName("dateTime", XmlSchemaNamespace)},
            {typeof (Decimal), new QName("decimal", XmlSchemaNamespace)},
            {typeof (Double), new QName("double", XmlSchemaNamespace)},
            {typeof (float), new QName("float", XmlSchemaNamespace)},
            {typeof (long), new QName("long", XmlSchemaNamespace)},
            {typeof (UInt32), new QName("unsignedInt", XmlSchemaNamespace)},
            {typeof (UInt16), new QName("unsignedShort", XmlSchemaNamespace)},
            {typeof (UInt64), new QName("unsignedLong", XmlSchemaNamespace)}
        };

        [Input(typeof(Int16))]
        [Input(typeof(Int32))]
        [Input(typeof(String))]
        [Input(typeof(Char))]
        [Input(typeof(Guid))]
        [Input(typeof(Boolean))]
        [Input(typeof(Byte))]
        [Input(typeof(DateTime))]
        [Input(typeof(Decimal))]
        [Input(typeof(Double))]
        [Input(typeof(float))]
        [Input(typeof(long))]
        [Input(typeof(UInt32))]
        [Input(typeof(UInt16))]
        [Input(typeof(UInt64))]
        public void GetQNameForType_ReturnsCorrectName_ForAllSoapPrimitiveType(Type primitiveType)
        {
            // arrange

            // act
            var actual = _sut.GetQNameForType(primitiveType);

            // assert
            actual.ShouldBeEquivalentTo(ExpectedQNames[primitiveType]);
        }

        public void GetPrimitiveTypesSchema_ReturnsTheStaticSchema()
        {
            // arrange
            var expected = new Schema
            {
                TargetNamespace = SerializationNamespace,
                QualifiedNamespaces = new List<QNamespace>
                {
                    new QNamespace("tns", SerializationNamespace)
                },
                Types = new List<SchemaType>
                {
                    CharType,
                    DurationType,
                    GuidType
                },
                Elements = new List<Element>
                {
                    AnyType,
                    AnyUri,
                    Base64Binary,
                    Boolean,
                    Byte,
                    DateTime,
                    Decimal,
                    Double,
                    Float,
                    Int,
                    Long,
                    QName,
                    Short,
                    String,
                    UnsignedByte,
                    UnsignedInt,
                    UnsignedLong,
                    UnsignedShort,
                    Char,
                    Duration,
                    Guid
                }
            };

            // act
            var actual = _sut.GetPrimitiveTypesSchema();

            // assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}