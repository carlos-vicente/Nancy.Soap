using System;
using WSDL.Models;

namespace WSDL.TypeManagement
{
    public interface IPrimitiveTypeProvider
    {
        Schema GetPrimitiveTypesSchema();
        QName GetQNameForType(Type type);
    }
}