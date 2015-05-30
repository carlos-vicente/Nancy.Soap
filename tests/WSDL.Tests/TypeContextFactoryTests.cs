using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using WSDL.TypeManagement;

namespace WSDL.Tests
{
    public class TypeContextFactoryTests
    {
        private readonly AutoFake _faker;
        private readonly TypeContextFactory _sut;

        public TypeContextFactoryTests()
        {
            _faker = new AutoFake();
            _sut = new TypeContextFactory(_faker.Resolve<IPrimitiveTypeProvider>());
        }

        public void Create_ReturnsTypeContext_WithPrimitiveTypeProvider()
        {
            // arrange
            var expected = new TypeContext(_faker.Resolve<IPrimitiveTypeProvider>());

            // act
            var context = _sut.Create();

            // assert
            context.As<TypeContext>().ShouldBeEquivalentTo(expected);
        }

        public void Create_ReturnsDifferentTypeContext_WhenCalledAgain()
        {
            // act
            var context1 = _sut.Create();
            var context2 = _sut.Create();

            // assert
            context2.Should().NotBe(context1);
        }
    }
}