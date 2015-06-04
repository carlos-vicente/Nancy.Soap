using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using WSDL.Models;
using WSDL.Models.Schema;
using WSDL.TypeManagement;

namespace WSDL.Tests.TypeContextTests
{
    public class SimpleContractTypeContextTests : TypeContextTests
    {
        private interface IContractToTest
        {
            void NoParamsNoReturn();
            string NoParamsWithReturn();
            void ParamsNoReturn(int integer, Guid guid);
            DateTime ParamsWithReturn(byte by, string something, bool boolean);
        }

        public void GetDescriptionForMethod_ObtainsCorrectDescription_ForMethodWithoutParametersAndWithoutReturn()
        {
            // arrange
            const string operationName = "NoParamsNoReturn";

            var methodInfo = typeof(IContractToTest)
                .GetMethod(operationName);

            var expected = new MethodDescription
            {
                Input = new ComplexType(operationName, new Sequence
                {
                    Elements = new List<Element>()
                }),
                Output = new ComplexType(operationName + "Response", new Sequence
                {
                    Elements = new List<Element>()
                })
            };

            // act
            var description = Sut.GetDescriptionForMethod(methodInfo, "namespace");

            // assert
            description.ShouldBeEquivalentTo(
                expected,
                opts => opts.IncludingAllRuntimeProperties());
        }

        public void GetDescriptionForMethod_ObtainsCorrectDescription_ForMethodWithoutParametersAndWithReturn()
        {
            // arrange
            const string operationName = "NoParamsWithReturn";

            var methodInfo = typeof(IContractToTest)
                .GetMethod(operationName);

            var qName = new QName("a");

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(string)))
                .Returns(qName);

            var outputSequence = new Sequence
            {
                Elements = new List<Element>
                {
                    new Element
                    {
                        Name = "NoParamsWithReturnResult",
                        Type = qName,
                        Nillable = true
                    }
                }
            };

            var expected = new MethodDescription
            {
                Input = new ComplexType(operationName, new Sequence
                {
                    Elements = new List<Element>()
                }),
                Output = new ComplexType(operationName + "Response", outputSequence)
            };

            // act
            var description = Sut.GetDescriptionForMethod(methodInfo, "namespace");

            // assert
            description.ShouldBeEquivalentTo(
                expected,
                opts => opts.IncludingAllRuntimeProperties());
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

            var expected = new MethodDescription
            {
                Input = new ComplexType(operationName, inputSequence),
                Output = new ComplexType(operationName + "Response", new Sequence
                {
                    Elements = new List<Element>()
                })
            };

            // act
            var description = Sut.GetDescriptionForMethod(methodInfo, "namespace");

            // assert
            description.ShouldBeEquivalentTo(
                expected,
                opts => opts.IncludingAllRuntimeProperties());
        }

        public void GetDescriptionForMethod_ObtainsCorrectDescription_ForMethodWithParametersAndWithReturn()
        {
            // arrange
            const string operationName = "ParamsWithReturn";

            var methodInfo = typeof(IContractToTest)
                .GetMethod(operationName);

            // DateTime ParamsWithReturn(byte by, string something, bool boolean);

            var byteName = new QName("a");
            var stringName = new QName("b");
            var boolName = new QName("c");
            var dateTimeName = new QName("d");

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(byte)))
                .Returns(byteName);

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(string)))
                .Returns(stringName);

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(bool)))
                .Returns(boolName);

            A.CallTo(() => Faker.Resolve<IPrimitiveTypeProvider>()
                .GetQNameForType(typeof(DateTime)))
                .Returns(dateTimeName);

            var inputSequence = new Sequence
            {
                Elements = new List<Element>
                {
                    new Element
                    {
                        Name = "by",
                        Type = byteName,
                        Nillable = false
                    },
                    new Element
                    {
                        Name = "something",
                        Type = stringName,
                        Nillable = true
                    },
                    new Element
                    {
                        Name = "boolean",
                        Type = boolName,
                        Nillable = false
                    }
                }
            };

            var outputSequence = new Sequence
            {
                Elements = new List<Element>
                {
                    new Element
                    {
                        Name = operationName + "Result",
                        Type = dateTimeName,
                        Nillable = true
                    }
                }
            };

            var expected = new MethodDescription
            {
                Input = new ComplexType(operationName, inputSequence),
                Output = new ComplexType(operationName + "Response", outputSequence)
            };

            // act
            var description = Sut.GetDescriptionForMethod(methodInfo, "namespace");

            // assert
            description.ShouldBeEquivalentTo(
                expected,
                opts => opts.IncludingAllRuntimeProperties());
        } 
    }
}