using System.Collections.Generic;
using System.Reflection;
using WSDL.Models;

namespace WSDL.TypeManagement
{
    public class TypeContext : ITypeContext
    {
        public TypeContext()
        {
            
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