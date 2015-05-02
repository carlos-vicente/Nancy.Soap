using AutoMapper;

namespace Nancy.SOAP.MappingProfiles
{
    public class DefinitionMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<
                WSDL.Contracts.Definition, 
                global::SOAP.Serialization.Definition>();

            CreateMap<
                WSDL.Contracts.QNamespace, 
                global::SOAP.Serialization.QNamespace>();

            CreateMap<
                WSDL.Contracts.Schema,
                global::SOAP.Serialization.Schema>();

            CreateMap<
                WSDL.Contracts.Message,
                global::SOAP.Serialization.Message>();

            CreateMap<
                WSDL.Contracts.PortType,
                global::SOAP.Serialization.PortType>();

            CreateMap<
                WSDL.Contracts.Binding,
                global::SOAP.Serialization.Binding>();

            CreateMap<
                WSDL.Contracts.Service,
                global::SOAP.Serialization.Service>();

            CreateMap<
                WSDL.Contracts.Element,
                global::SOAP.Serialization.Element>();

            CreateMap<
                WSDL.Contracts.QName,
                global::SOAP.Serialization.QName>();

            CreateMap<
                WSDL.Contracts.SchemaType,
                global::SOAP.Serialization.SchemaType>()
                .Include<
                    WSDL.Contracts.ComplexType,
                    global::SOAP.Serialization.ComplexType>()
                .Include<
                    WSDL.Contracts.SimpleType,
                    global::SOAP.Serialization.SimpleType>();

            CreateMap<
                WSDL.Contracts.ComplexType,
                global::SOAP.Serialization.ComplexType>();

            CreateMap<
                WSDL.Contracts.SimpleContent,
                global::SOAP.Serialization.SimpleContent>();

            CreateMap<
                WSDL.Contracts.ComplexContent,
                global::SOAP.Serialization.ComplexContent>();

            CreateMap<
                WSDL.Contracts.ElementGrouping,
                global::SOAP.Serialization.ElementGrouping>()
                .Include<
                    WSDL.Contracts.All,
                    global::SOAP.Serialization.All>()
                .Include<
                    WSDL.Contracts.Choice,
                    global::SOAP.Serialization.Choice>()
                .Include<
                    WSDL.Contracts.Group,
                    global::SOAP.Serialization.Group>()
                .Include<
                    WSDL.Contracts.Sequence,
                    global::SOAP.Serialization.Sequence>();

            CreateMap<
                WSDL.Contracts.All,
                global::SOAP.Serialization.All>();

            CreateMap<
                WSDL.Contracts.Choice,
                global::SOAP.Serialization.Choice>();

            CreateMap<
                WSDL.Contracts.Group,
                global::SOAP.Serialization.Group>();

            CreateMap<
                WSDL.Contracts.Sequence,
                global::SOAP.Serialization.Sequence>();

            CreateMap<
                WSDL.Contracts.SimpleType,
                global::SOAP.Serialization.SimpleType>();

            CreateMap<
                WSDL.Contracts.Restriction,
                global::SOAP.Serialization.Restriction>();

            CreateMap<
                WSDL.Contracts.List,
                global::SOAP.Serialization.List>();

            CreateMap<
                WSDL.Contracts.Union,
                global::SOAP.Serialization.Union>();

            CreateMap<
                WSDL.Contracts.WhiteSpaceConstraint,
                global::SOAP.Serialization.WhiteSpaceConstraint>();

            CreateMap<
                WSDL.Contracts.MessagePart,
                global::SOAP.Serialization.MessagePart>()
                .Include<
                    WSDL.Contracts.TypeMessagePart,
                    global::SOAP.Serialization.TypeMessagePart>()
                .Include<
                    WSDL.Contracts.ElementMessagePart,
                    global::SOAP.Serialization.ElementMessagePart>();

            CreateMap<
                WSDL.Contracts.TypeMessagePart,
                global::SOAP.Serialization.TypeMessagePart>();

            CreateMap<
                WSDL.Contracts.ElementMessagePart,
                global::SOAP.Serialization.ElementMessagePart>();

            CreateMap<
                WSDL.Contracts.Operation,
                global::SOAP.Serialization.Operation>()
                .Include<
                    WSDL.Contracts.RequestResponseOperation,
                    global::SOAP.Serialization.RequestResponseOperation>();

            CreateMap<
                WSDL.Contracts.RequestResponseOperation,
                global::SOAP.Serialization.RequestResponseOperation>();

            CreateMap<
                WSDL.Contracts.OperationMessage,
                global::SOAP.Serialization.OperationMessage>();

            CreateMap<
                WSDL.Contracts.Binding,
                global::SOAP.Serialization.Binding>();

            CreateMap<
                WSDL.Contracts.Service,
                global::SOAP.Serialization.Service>();
        }
    }
}