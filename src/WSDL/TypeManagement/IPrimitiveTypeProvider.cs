using System;
using WSDL.Models;
using WSDL.Models.Schema;

namespace WSDL.TypeManagement
{
    public interface IPrimitiveTypeProvider
    {
        Schema GetPrimitiveTypesSchema();
        QName GetQNameForType(Type type);
        bool IsPrimitive(Type type);
    }
}