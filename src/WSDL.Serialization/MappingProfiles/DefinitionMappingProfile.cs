using AutoMapper;

namespace WSDL.Serialization.MappingProfiles
{
    public class DefinitionMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<
                WSDL.Models.Definition, 
                WSDL.Serialization.Definition>();

            CreateMap<
                WSDL.Models.QNamespace, 
                WSDL.Serialization.QNamespace>();

            CreateMap<
                WSDL.Models.Schema,
                WSDL.Serialization.Schema>();

            CreateMap<
                WSDL.Models.Message,
                WSDL.Serialization.Message>();

            CreateMap<
                WSDL.Models.PortType,
                WSDL.Serialization.PortType>();

            CreateMap<
                WSDL.Models.Binding,
                WSDL.Serialization.Binding>();

            CreateMap<
                WSDL.Models.Service,
                WSDL.Serialization.Service>();

            CreateMap<
                WSDL.Models.Element,
                WSDL.Serialization.Element>();

            CreateMap<
                WSDL.Models.QName,
                WSDL.Serialization.QName>();

            CreateMap<
                WSDL.Models.SchemaType,
                WSDL.Serialization.SchemaType>()
                .Include<
                    WSDL.Models.ComplexType,
                    WSDL.Serialization.ComplexType>()
                .Include<
                    WSDL.Models.SimpleType,
                    WSDL.Serialization.SimpleType>();

            CreateMap<
                WSDL.Models.ComplexType,
                WSDL.Serialization.ComplexType>();

            CreateMap<
                WSDL.Models.SimpleContent,
                WSDL.Serialization.SimpleContent>();

            CreateMap<
                WSDL.Models.ComplexContent,
                WSDL.Serialization.ComplexContent>();

            CreateMap<
                WSDL.Models.ElementGrouping,
                WSDL.Serialization.ElementGrouping>()
                .Include<
                    WSDL.Models.All,
                    WSDL.Serialization.All>()
                .Include<
                    WSDL.Models.Choice,
                    WSDL.Serialization.Choice>()
                .Include<
                    WSDL.Models.Group,
                    WSDL.Serialization.Group>()
                .Include<
                    WSDL.Models.Sequence,
                    WSDL.Serialization.Sequence>();

            CreateMap<
                WSDL.Models.All,
                WSDL.Serialization.All>();

            CreateMap<
                WSDL.Models.Choice,
                WSDL.Serialization.Choice>();

            CreateMap<
                WSDL.Models.Group,
                WSDL.Serialization.Group>();

            CreateMap<
                WSDL.Models.Sequence,
                WSDL.Serialization.Sequence>();

            CreateMap<
                WSDL.Models.SimpleType,
                WSDL.Serialization.SimpleType>();

            CreateMap<
                WSDL.Models.Restriction,
                WSDL.Serialization.Restriction>();

            CreateMap<
                WSDL.Models.List,
                WSDL.Serialization.List>();

            CreateMap<
                WSDL.Models.Union,
                WSDL.Serialization.Union>();

            CreateMap<
                WSDL.Models.WhiteSpaceConstraint,
                WSDL.Serialization.WhiteSpaceConstraint>();

            CreateMap<
                WSDL.Models.MessagePart,
                WSDL.Serialization.MessagePart>()
                .Include<
                    WSDL.Models.TypeMessagePart,
                    WSDL.Serialization.TypeMessagePart>()
                .Include<
                    WSDL.Models.ElementMessagePart,
                    WSDL.Serialization.ElementMessagePart>();

            CreateMap<
                WSDL.Models.TypeMessagePart,
                WSDL.Serialization.TypeMessagePart>();

            CreateMap<
                WSDL.Models.ElementMessagePart,
                WSDL.Serialization.ElementMessagePart>();

            CreateMap<
                WSDL.Models.Operation,
                WSDL.Serialization.Operation>()
                .Include<
                    WSDL.Models.RequestResponseOperation,
                    WSDL.Serialization.RequestResponseOperation>();

            CreateMap<
                WSDL.Models.RequestResponseOperation,
                WSDL.Serialization.RequestResponseOperation>();

            CreateMap<
                WSDL.Models.OperationMessage,
                WSDL.Serialization.OperationMessage>();

            CreateMap<
                WSDL.Models.Binding,
                WSDL.Serialization.Binding>();

            CreateMap<
                WSDL.Models.Service,
                WSDL.Serialization.Service>();
        }
    }
}