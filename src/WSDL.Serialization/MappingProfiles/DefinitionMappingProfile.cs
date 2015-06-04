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
                Models.Schema.Schema,
                WSDL.Serialization.Schema>();

            CreateMap<
                Models.Message.Message,
                WSDL.Serialization.Message>();

            CreateMap<
                Models.PortType.PortType,
                WSDL.Serialization.PortType>();

            CreateMap<
                Models.Binding.Binding,
                WSDL.Serialization.Binding>();

            CreateMap<
                Models.Service.Service,
                WSDL.Serialization.Service>();

            CreateMap<
                Models.Schema.Element,
                WSDL.Serialization.Element>();

            CreateMap<
                WSDL.Models.QName,
                WSDL.Serialization.QName>();

            CreateMap<
                Models.Schema.SchemaType,
                WSDL.Serialization.SchemaType>()
                .Include<
                    Models.Schema.ComplexType,
                    WSDL.Serialization.ComplexType>()
                .Include<
                    Models.Schema.SimpleType,
                    WSDL.Serialization.SimpleType>();

            CreateMap<
                Models.Schema.ComplexType,
                WSDL.Serialization.ComplexType>();

            CreateMap<
                Models.Schema.SimpleContent,
                WSDL.Serialization.SimpleContent>();

            CreateMap<
                Models.Schema.ComplexContent,
                WSDL.Serialization.ComplexContent>();

            CreateMap<
                Models.Schema.ElementGrouping,
                WSDL.Serialization.ElementGrouping>()
                .Include<
                    Models.Schema.All,
                    WSDL.Serialization.All>()
                .Include<
                    Models.Schema.Choice,
                    WSDL.Serialization.Choice>()
                .Include<
                    Models.Schema.Group,
                    WSDL.Serialization.Group>()
                .Include<
                    Models.Schema.Sequence,
                    WSDL.Serialization.Sequence>();

            CreateMap<
                Models.Schema.All,
                WSDL.Serialization.All>();

            CreateMap<
                Models.Schema.Choice,
                WSDL.Serialization.Choice>();

            CreateMap<
                Models.Schema.Group,
                WSDL.Serialization.Group>();

            CreateMap<
                Models.Schema.Sequence,
                WSDL.Serialization.Sequence>();

            CreateMap<
                Models.Schema.SimpleType,
                WSDL.Serialization.SimpleType>();

            CreateMap<
                Models.Schema.Restriction,
                WSDL.Serialization.Restriction>();

            CreateMap<
                Models.Schema.List,
                WSDL.Serialization.List>();

            CreateMap<
                Models.Schema.Union,
                WSDL.Serialization.Union>();

            CreateMap<
                Models.Schema.WhiteSpaceConstraint,
                WSDL.Serialization.WhiteSpaceConstraint>();

            CreateMap<
                Models.Message.MessagePart,
                WSDL.Serialization.MessagePart>()
                .Include<
                    Models.Message.TypeMessagePart,
                    WSDL.Serialization.TypeMessagePart>()
                .Include<
                    Models.Message.ElementMessagePart,
                    WSDL.Serialization.ElementMessagePart>();

            CreateMap<
                Models.Message.TypeMessagePart,
                WSDL.Serialization.TypeMessagePart>();

            CreateMap<
                Models.Message.ElementMessagePart,
                WSDL.Serialization.ElementMessagePart>();

            CreateMap<
                Models.PortType.Operation,
                WSDL.Serialization.Operation>()
                .Include<
                    Models.PortType.RequestResponseOperation,
                    WSDL.Serialization.RequestResponseOperation>();

            CreateMap<
                Models.PortType.RequestResponseOperation,
                WSDL.Serialization.RequestResponseOperation>();

            CreateMap<
                Models.PortType.OperationMessage,
                WSDL.Serialization.OperationMessage>();

            CreateMap<
                Models.Binding.Binding,
                WSDL.Serialization.Binding>();

            CreateMap<
                Models.Service.Service,
                WSDL.Serialization.Service>();
        }
    }
}