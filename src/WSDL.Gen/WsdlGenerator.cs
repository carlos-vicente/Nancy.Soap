using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSDL.Contracts;

namespace WSDL.Gen
{
    public class WsdlGenerator : IWsdlGenerator
    {
        private readonly IPrimitiveTypeProvider _primitiveTypeProvider;
        
        public WsdlGenerator(IPrimitiveTypeProvider primitiveTypeProvider)
        {
            _primitiveTypeProvider = primitiveTypeProvider;
        }

        public async Task<Definition> GetWebServiceDefinition(Type contract)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");

            if (!contract.IsInterface)
                throw new ArgumentException(
                    "An interface must be provided to generate a WSDL",
                    "contract");

            var primitiveTypesSchema = _primitiveTypeProvider.GetPrimitiveTypesSchema();

            var types = new List<SchemaType>();
            var elements = new List<Element>();

            var messages = new List<Message>();
            var operations = new List<Operation>();

            foreach (var method in contract.GetMethods())
            {
                var inputParametersElements =
                    (from parameter in method.GetParameters()
                        let typeName = _primitiveTypeProvider
                            .GetQNameForType(parameter.ParameterType)
                        where typeName != null
                        select new Element
                        {
                            Name = parameter.Name,
                            Type = new QName(typeName.Name, "tns")
                        })
                        .ToList();

                var inputType = new ComplexType(
                    string.Format("{0}", method.Name),
                    new Sequence
                    {
                        Elements = inputParametersElements
                    });

                var inputElement = new Element
                {
                    Name = inputType.Name,
                    Type = new QName(inputType.Name, "tns")
                };

                types.Add(inputType);
                elements.Add(inputElement);

                var inputMessage = new Message
                {
                    Name = string.Format("{0}_{1}_InputMessage", contract.Name, method.Name),
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName(inputElement.Name, "tns"))
                    }
                };


                var outputParametersElement = new Element
                {
                    Name = string.Format("{0}Result", method.Name),
                    Type = _primitiveTypeProvider
                        .GetQNameForType(method.ReturnType)
                };

                var outputType = new ComplexType(
                    string.Format("{0}Response", method.Name),
                    new Sequence
                    {
                        Elements = new List<Element> { outputParametersElement }
                    });

                var outputElement = new Element
                {
                    Name = outputType.Name,
                    Type = new QName(outputType.Name, "tns")
                };

                types.Add(outputType);
                elements.Add(outputElement);

                var outputMessage = new Message
                {
                    Name = string.Format("{0}_{1}_OutputMessage", contract.Name, method.Name),
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName(outputElement.Name, "tns"))
                    }
                };

                operations.Add(new RequestResponseOperation
                {
                    Name = method.Name,
                    Input = new OperationMessage
                    {
                        Action = string.Format(
                            "{0}/{1}/{2}",
                            Definition.DefaultNamespace,
                            contract.Name,
                            method.Name),
                        Message = new QName(inputMessage.Name, "tns")
                    },
                    Output = new OperationMessage
                    {
                        Action = string.Format(
                            "{0}/{1}/{2}Response",
                            Definition.DefaultNamespace,
                            contract.Name,
                            method.Name),
                        Message = new QName(outputMessage.Name, "tns")
                    }
                });

                messages.Add(inputMessage);
                messages.Add(outputMessage);
            }

            var messageTypes = new Schema
            {
                TargetNamespace = Definition.DefaultNamespace,
                QualifiedNamespaces = new List<QNamespace>
                {
                    new QNamespace("xs", "http://www.w3.org/2001/XMLSchema")
                },
                Types = types,
                Elements = elements
            };

            var portType = new PortType
            {
                Name = contract.Name,
                Operations = operations
            };

            var definition = new Definition
            {
                Types = new List<Schema>
                {
                    primitiveTypesSchema,
                    messageTypes
                },
                Messages = messages,
                PortTypes = new List<PortType> { portType },
                Bindings = new List<Binding>(),
                Services = new List<Service>()
            };

            return definition;
        }
    }
}
