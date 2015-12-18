using System;
using AutoMapper;
using WSDL.Serialization.Schema;

namespace WSDL.Serialization.MappingProfiles.Converters
{
    public class SchemaTypeConverter : ITypeConverter<Models.Schema.SchemaType, SchemaType>
    {
        public SchemaType Convert(ResolutionContext context)
        {
            var source = context.SourceValue as Models.Schema.SchemaType;

            if (source is Models.Schema.SimpleType)
                return null;

            return null;
        }
    }
}
