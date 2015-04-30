using System;
using WSDL.Contracts;

namespace WSDL.Gen
{
    public interface IPrimitiveTypeProvider
    {
        Schema GetPrimitiveTypesSchema();
        QName GetQNameForType(Type type);
    }
}