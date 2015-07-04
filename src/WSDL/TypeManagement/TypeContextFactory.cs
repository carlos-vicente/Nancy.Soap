namespace WSDL.TypeManagement
{
    public class TypeContextFactory : ITypeContextFactory
    {
        private readonly IPrimitiveTypeProvider _primitiveTypeProvider;

        public TypeContextFactory(IPrimitiveTypeProvider primitiveTypeProvider)
        {
            _primitiveTypeProvider = primitiveTypeProvider;
        }

        public ITypeContext Create()
        {
            return new TypeContext(_primitiveTypeProvider);
        }
    }
}