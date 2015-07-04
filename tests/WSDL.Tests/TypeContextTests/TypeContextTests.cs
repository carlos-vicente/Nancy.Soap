using Autofac.Extras.FakeItEasy;
using WSDL.TypeManagement;

namespace WSDL.Tests.TypeContextTests
{
    public abstract class TypeContextTests
    {
        protected readonly AutoFake Faker;
        protected readonly TypeContext Sut;

        protected TypeContextTests()
        {
            Faker = new AutoFake();
            Sut = Faker.Resolve<TypeContext>();
        }
    }
}