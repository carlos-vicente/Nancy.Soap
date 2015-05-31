using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using WSDL.Models;
using WSDL.TypeManagement;

namespace WSDL.Tests.TypeContextTests
{
    public class SchemaTypeContextTests : TypeContextTests
    {
        private interface IContractToTest
        {
            void ParamsNoReturn(int integer, Guid guid);
        }

        public void GetDescriptionForMethod_ObtainsCorrectDescription_ForMethodWithParametersAndWithoutReturn()
        {
            // arrange
            const string operationName = "ParamsNoReturn";

            var methodInfo = typeof(IContractToTest)
                .GetMethod(operationName);

            var intName = new QName("a");
            var guidName = new QName("b");

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(int)))
                .Returns(intName);

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(Guid)))
                .Returns(guidName);

            const string @namespace = "namespace";

            var inputSequence = new Sequence
            {
                Elements = new List<Element>
                {
                    new Element
                    {
                        Name = "integer",
                        Type = intName,
                        Nillable = false
                    },
                    new Element
                    {
                        Name = "guid",
                        Type = guidName,
                        Nillable = true
                    }
                }
            };

            var variableSchema = new Schema
            {
                TargetNamespace = @namespace,
                Types = new List<SchemaType>
                {
                    new ComplexType(operationName, inputSequence),
                    new ComplexType(operationName + "Response", new Sequence
                    {
                        Elements = new List<Element>()
                    })
                },
                Elements = new List<Element>
                {
                    new Element
                    {
                        Name = operationName,
                        Type = new QName(operationName, @namespace)
                    },
                    new Element
                    {
                        Name = operationName + "Response",
                        Type = new QName(operationName + "Response", @namespace)
                    }
                }
            };

            var staticSchema = new Schema();

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetPrimitiveTypesSchema())
                .Returns(staticSchema);
            
            Sut.GetDescriptionForMethod(methodInfo, @namespace);

            var expected = new List<Schema>
            {
                staticSchema,
                variableSchema
            };

            // act
            var schemas = Sut.GetSchemas();

            // assert
            schemas.ShouldAllBeEquivalentTo(
                expected, 
                opts => opts.IncludingAllRuntimeProperties());
        }
    }
}