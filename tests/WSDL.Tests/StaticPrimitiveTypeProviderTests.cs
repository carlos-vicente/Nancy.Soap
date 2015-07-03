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

        private readonly StaticPrimitiveTypeProvider _sut;
        private readonly AutoFake _faker;

        public StaticPrimitiveTypeProviderTests()
        {
            _faker = new AutoFake();
            _sut = _faker.Resolve<StaticPrimitiveTypeProvider>();
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
            var actual = this._sut.IsPrimitive(primitiveType);

            // assert
            actual.Should().BeTrue();
        }

        [Input(typeof(StaticPrimitiveTypeProvider))]
        [Input(typeof(IPrimitiveTypeProvider))]
        public void IsPrimitive_ReturnsFalse_ForTypesNotInSoapPrimitiveCollection(Type nonPrimitiveType)
        {
            // arrange

            // act
            var actual = this._sut.IsPrimitive(nonPrimitiveType);

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
            var actual = this._sut.GetQNameForType(primitiveType);

            // assert
            actual.ShouldBeEquivalentTo(ExpectedQNames[primitiveType]);
        }

        public void GetPrimitiveTypesSchema_ReturnsTheStaticSchema()
        {
            // arrange
            var expected = new Schema
            {

            };

            // act
            var actual = this._sut.GetPrimitiveTypesSchema();

            // assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}