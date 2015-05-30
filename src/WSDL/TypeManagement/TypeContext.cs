using System.Collections.Generic;
using System.Reflection;
using WSDL.Models;

namespace WSDL.TypeManagement
{
    public class TypeContext : ITypeContext
    {
        public IPrimitiveTypeProvider PrimitiveTypeProvider { get; private set; }

        public TypeContext(IPrimitiveTypeProvider primitiveTypeProvider)
        {
            PrimitiveTypeProvider = primitiveTypeProvider;
        }

        public MethodDescription GetDescriptionForMethod(MethodInfo methodInfo, string contractNamespace)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Schema> GetSchemas()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}