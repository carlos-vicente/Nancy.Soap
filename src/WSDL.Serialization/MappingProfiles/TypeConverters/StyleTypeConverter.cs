using AutoMapper;
using WSDL.Serialization.Binding;

namespace WSDL.Serialization.MappingProfiles.TypeConverters
{
    public class StyleTypeConverter : ITypeConverter<Models.Binding.Style?, Style?>
    {
        public Style? Convert(ResolutionContext context)
        {
            var source = (Models.Binding.Style?)context.SourceValue;

            if (!source.HasValue)
                return null;

            var intSourceValue = (int) source;

            return (Style)intSourceValue;
        }
    }
}
