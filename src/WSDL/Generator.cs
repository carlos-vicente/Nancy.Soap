using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSDL.Models;
using WSDL.TypeManagement;

namespace WSDL
{
    public class Generator : IGenerator
    {
        private readonly ITypeContextFactory _typeContextFactory;
        public static readonly string DefaultNamespace = "http://tempuri.org";
        //public static readonly string AddressingNamespace = "http://www.w3.org/2006/05/addressing/wsdl";

        //private readonly IPrimitiveTypeProvider _primitiveTypeProvider;
        
        //public Generator(IPrimitiveTypeProvider primitiveTypeProvider)
        //{
        //    _primitiveTypeProvider = primitiveTypeProvider;
        //}

        public Generator(ITypeContextFactory typeContextFactory)
        {
            _typeContextFactory = typeContextFactory;
        }

        public async Task<Definition> GetWebServiceDefinition(Type contract)
        {
            return await GetWebServiceDefinition(contract, DefaultNamespace);
        }

        public async Task<Definition> GetWebServiceDefinition(
            Type contract,
            string contractNamespace)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");

            if (!contract.IsInterface)
                throw new ArgumentException(
                    "An interface must be provided to generate a WSDL",
                    "contract");

            //var primitiveTypesSchema = _primitiveTypeProvider.GetPrimitiveTypesSchema();

            //var types = new List<SchemaType>();
            //var elements = new List<Element>();

            IEnumerable<Schema> schemas;

            var messages = new List<Message>();
            var operations = new List<Operation>();

            using (var typeContext = _typeContextFactory.Create())
            {
                foreach (var method in contract.GetMethods())
                {
                    ///// getting input elements to describe parameters
                    //var inputParametersElements =
                    //    (from parameter in method.GetParameters()
                    //        let typeName = _primitiveTypeProvider
                    //            .GetQNameForType(parameter.ParameterType)
                    //        where typeName != null
                    //        select new Element
                    //        {
                    //            Name = parameter.Name,
                    //            Type = typeName,
                    //            Nillable = !parameter.ParameterType.IsPrimitive
                    //        })
                    //        .ToList();

                    ///// getting input complex type
                    //var inputType = new ComplexType(
                    //    string.Format("{0}", method.Name),
                    //    new Sequence
                    //    {
                    //        Elements = inputParametersElements
                    //    });

                    var methodDescription = typeContext
                        .GetDescriptionForMethod(method, contractNamespace);

                    ///// getting element to reference input complex type
                    var inputElement = new Element
                    {
                        Name = methodDescription.Input.Name,
                        Type = new QName(methodDescription.Input.Name, contractNamespace)
                    };

                    //types.Add(methodDescription.Input);
                    //elements.Add(inputElement);

                    ///// getting input message to relate to input element
                    var inputMessage = new Message
                    {
                        Name = string.Format("{0}_{1}_InputMessage", contract.Name, method.Name),
                        Parts = new List<MessagePart>
                        {
                            new ElementMessagePart("parameters", new QName(inputElement.Name, contractNamespace))
                        }
                    };

                    // output

                    ///// getting output element to describe return type
                    //var outputParametersElements = new List<Element>();
                    //if(method.ReturnType != typeof(void))
                    //    outputParametersElements.Add(new Element
                    //    {
                    //        Name = string.Format("{0}Result", method.Name),
                    //        Type = _primitiveTypeProvider
                    //            .GetQNameForType(method.ReturnType),
                    //        Nillable = !method.ReturnType.IsPrimitive
                    //    });

                    ///// getting output complex type
                    //var outputType = new ComplexType(
                    //    string.Format("{0}Response", method.Name),
                    //    new Sequence
                    //    {
                    //        Elements = outputParametersElements
                    //    });

                    // getting element to reference output complex type
                    var outputElement = new Element
                    {
                        Name = methodDescription.Output.Name,
                        Type = new QName(methodDescription.Output.Name, contractNamespace)
                    };

                    //types.Add(methodDescription.Output);
                    //elements.Add(outputElement);

                    // getting output message to relate to output element
                    var outputMessage = new Message
                    {
                        Name = string.Format("{0}_{1}_OutputMessage", contract.Name, method.Name),
                        Parts = new List<MessagePart>
                        {
                            new ElementMessagePart("parameters", new QName(outputElement.Name, contractNamespace))
                        }
                    };

                    // getting operation for port type
                    operations.Add(new RequestResponseOperation
                    {
                        Name = method.Name,
                        Input = new OperationMessage
                        {
                            DirectionType = "Input",
                            Action = string.Format(
                                "{0}/{1}/{2}",
                                contractNamespace,
                                contract.Name,
                                method.Name),
                            Message = new QName(inputMessage.Name, contractNamespace)
                        },
                        Output = new OperationMessage
                        {
                            DirectionType = "Output",
                            Action = string.Format(
                                "{0}/{1}/{2}Response",
                                contractNamespace,
                                contract.Name,
                                method.Name),
                            Message = new QName(outputMessage.Name, contractNamespace)
                        }
                    });

                    messages.Add(inputMessage);
                    messages.Add(outputMessage);
                }
                schemas = typeContext.GetSchemas();
            }

            //var messageTypes = new Schema
            //{
            //    TargetNamespace = contractNamespace,
            //    Types = types,
            //    Elements = elements
            //};

            var portType = new PortType
            {
                Name = contract.Name,
                Operations = operations
            };

            var definition = new Definition
            {
                TargetNamespace = contractNamespace,
                QualifiedNamespaces = new List<QNamespace>
                {
                    new QNamespace("tns", contractNamespace),
                    //new QNamespace("wsaw", AddressingNamespace)
                },
                Types = schemas,
                Messages = messages,
                PortTypes = new List<PortType> {portType},
                Bindings = new List<Binding>(),
                Services = new List<Service>()
            };

            return definition;
        }
    }
}
