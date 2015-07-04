using AutoMapper;
using WSDL.Serialization.Message;
using WSDL.Serialization.PortType;
using WSDL.Serialization.Schema;

namespace WSDL.Serialization.MappingProfiles
{
    public class DefinitionMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<
                Models.Definition, 
                Definition>();

            CreateMap<
                Models.QNamespace, 
                QNamespace>();

            CreateMap<
                Models.Schema.Schema,
                Schema.Schema>();

            CreateMap<
                Models.Message.Message,
                Message.Message>();

            CreateMap<
                Models.PortType.PortType,
                PortType.PortType>();

            CreateMap<
                Models.Schema.Element,
                Element>();

            CreateMap<
                Models.QName,
                QName>();

            CreateMap<
                Models.Schema.SchemaType,
                SchemaType>()
                .Include<
                    Models.Schema.ComplexType,
                    ComplexType>()
                .Include<
                    Models.Schema.SimpleType,
                    SimpleType>();

            CreateMap<
                Models.Schema.ComplexType,
                ComplexType>();

            CreateMap<
                Models.Schema.SimpleContent,
                SimpleContent>();

            CreateMap<
                Models.Schema.ComplexContent,
                ComplexContent>();

            CreateMap<
                Models.Schema.ElementGrouping,
                ElementGrouping>()
                .Include<
                    Models.Schema.All,
                    All>()
                .Include<
                    Models.Schema.Choice,
                    Choice>()
                .Include<
                    Models.Schema.Group,
                    Group>()
                .Include<
                    Models.Schema.Sequence,
                    Sequence>();

            CreateMap<
                Models.Schema.All,
                All>();

            CreateMap<
                Models.Schema.Choice,
                Choice>();

            CreateMap<
                Models.Schema.Group,
                Group>();

            CreateMap<
                Models.Schema.Sequence,
                Sequence>();

            CreateMap<
                Models.Schema.SimpleType,
                SimpleType>();

            CreateMap<
                Models.Schema.Restriction,
                Restriction>();

            CreateMap<
                Models.Schema.List,
                List>();

            CreateMap<
                Models.Schema.Union,
                Union>();

            CreateMap<
                Models.Schema.WhiteSpaceConstraint,
                WhiteSpaceConstraint>();

            CreateMap<
                Models.Message.MessagePart,
                MessagePart>()
                .Include<
                    Models.Message.TypeMessagePart,
                    TypeMessagePart>()
                .Include<
                    Models.Message.ElementMessagePart,
                    ElementMessagePart>();

            CreateMap<
                Models.Message.TypeMessagePart,
                TypeMessagePart>();

            CreateMap<
                Models.Message.ElementMessagePart,
                ElementMessagePart>();

            CreateMap<
                Models.PortType.Operation,
                Operation>()
                .Include<
                    Models.PortType.RequestResponseOperation,
                    RequestResponseOperation>();

            CreateMap<
                Models.PortType.RequestResponseOperation,
                RequestResponseOperation>()
                .AfterMap((mrro, rro) =>
                {
                    rro.Input.ElementName = "Input";
                    rro.Output.ElementName = "Output";
                });

            CreateMap<
                Models.PortType.OperationMessage,
                OperationMessage>();

            CreateMap<
                Models.Binding.Binding,
                Binding.Binding>();

            CreateMap<
                Models.Binding.Operation,
                Binding.Operation>()
                .Include<
                    Models.Binding.RequestResponseOperation,
                    Binding.RequestResponseOperation>();

            CreateMap<
                Models.Binding.RequestResponseOperation,
                Binding.RequestResponseOperation>()
                .AfterMap((mrro, rro) =>
                {
                    rro.Input.ElementName = "Input";
                    rro.Output.ElementName = "Output";
                });

            CreateMap<
                Models.Binding.OperationMessage,
                Binding.OperationMessage>();

            CreateMap<
                Models.Binding.OperationFaultMessage,
                Binding.OperationFaultMessage>();

            CreateMap<
                Models.Binding.Style,
                Binding.Style>();

            CreateMap<
                Models.Binding.Transport,
                Binding.Transport>();

            CreateMap<
                Models.Binding.SoapExtensions.Binding,
                Binding.SoapExtensions.Binding>();

            CreateMap<
                Models.Binding.SoapExtensions.Operation,
                Binding.SoapExtensions.Operation>();

            CreateMap<
                Models.Binding.SoapExtensions.Body,
                Binding.SoapExtensions.Body>();

            CreateMap<
                Models.Binding.SoapExtensions.Fault,
                Binding.SoapExtensions.Fault>();

            CreateMap<
                Models.Binding.SoapExtensions.Header,
                Binding.SoapExtensions.Header>();

            CreateMap<
                Models.Binding.SoapExtensions.OperationMessageUse,
                Binding.SoapExtensions.OperationMessageUse>();

            CreateMap<
                Models.Service.Service,
                Service.Service>();

            CreateMap<
                Models.Service.Port,
                Service.Port>();

            CreateMap<
                Models.Service.SoapExtensions.Address,
                Service.SoapExtensions.Address>();
        }
    }
}