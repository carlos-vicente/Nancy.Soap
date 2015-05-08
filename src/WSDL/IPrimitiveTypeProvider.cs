using System;
using WSDL.Models;

namespace WSDL
{
    public interface IPrimitiveTypeProvider
    {
        Schema GetPrimitiveTypesSchema();
        QName GetQNameForType(Type type);
    }
}