﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSDL.Models;

namespace WSDL
{
    public class Generator : IGenerator
    {
        private readonly IPrimitiveTypeProvider _primitiveTypeProvider;
        
        public Generator(IPrimitiveTypeProvider primitiveTypeProvider)
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
                // getting input elements to describe parameters
                var inputParametersElements =
                    (from parameter in method.GetParameters()
                        let typeName = _primitiveTypeProvider
                            .GetQNameForType(parameter.ParameterType)
                        where typeName != null
                        select new Element
                        {
                            Name = parameter.Name,
                            Type = typeName,
                            Nillable = !parameter.ParameterType.IsPrimitive
                        })
                        .ToList();
                
                // getting input complex type
                var inputType = new ComplexType(
                    string.Format("{0}", method.Name),
                    new Sequence
                    {
                        Elements = inputParametersElements
                    });

                // getting element to reference input complex type
                var inputElement = new Element
                {
                    Name = inputType.Name,
                    Type = new QName(inputType.Name, Definition.DefaultNamespace)
                };

                types.Add(inputType);
                elements.Add(inputElement);

                // getting input message to relate to input element
                var inputMessage = new Message
                {
                    Name = string.Format("{0}_{1}_InputMessage", contract.Name, method.Name),
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName(inputElement.Name, Definition.DefaultNamespace))
                    }
                };

                // getting output element to describe return type
                var outputParametersElements = new List<Element>();
                if(method.ReturnType != typeof(void))
                    outputParametersElements.Add(new Element
                    {
                        Name = string.Format("{0}Result", method.Name),
                        Type = _primitiveTypeProvider
                            .GetQNameForType(method.ReturnType)
                    });

                // getting output complex type
                var outputType = new ComplexType(
                    string.Format("{0}Response", method.Name),
                    new Sequence
                    {
                        Elements = outputParametersElements
                    });

                // getting element to reference output complex type
                var outputElement = new Element
                {
                    Name = outputType.Name,
                    Type = new QName(outputType.Name, Definition.DefaultNamespace)
                };

                types.Add(outputType);
                elements.Add(outputElement);

                // getting output message to relate to output element
                var outputMessage = new Message
                {
                    Name = string.Format("{0}_{1}_OutputMessage", contract.Name, method.Name),
                    Parts = new List<MessagePart>
                    {
                        new ElementMessagePart("parameters", new QName(outputElement.Name, Definition.DefaultNamespace))
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
                        Message = new QName(inputMessage.Name, Definition.DefaultNamespace)
                    },
                    Output = new OperationMessage
                    {
                        Action = string.Format(
                            "{0}/{1}/{2}Response",
                            Definition.DefaultNamespace,
                            contract.Name,
                            method.Name),
                        Message = new QName(outputMessage.Name, Definition.DefaultNamespace)
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
                    //new QNamespace("xs", "http://www.w3.org/2001/XMLSchema")
                    new QNamespace("tns", Definition.DefaultNamespace)
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
                QualifiedNamespaces = new List<QNamespace>
                {
                    
                },
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
