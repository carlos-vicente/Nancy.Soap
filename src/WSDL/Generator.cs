using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WSDL.Models;
using WSDL.Models.Binding;
using WSDL.Models.Message;
using WSDL.Models.PortType;
using WSDL.Models.Schema;
using WSDL.Models.Service;
using WSDL.Models.Service.SoapExtensions;
using WSDL.TypeManagement;
using RequestResponseOperation = WSDL.Models.PortType.RequestResponseOperation;

namespace WSDL
{
    public class Generator : IGenerator
    {
        private readonly ITypeContextFactory _typeContextFactory;

        private const string DefaultNamespace = "http://tempuri.org";

        public Generator(ITypeContextFactory typeContextFactory)
        {
            _typeContextFactory = typeContextFactory;
        }

        public async Task<Definition> GetWebServiceDefinition(Type contract, string endpoint)
        {
            return await GetWebServiceDefinition(contract, endpoint, DefaultNamespace);
        }

        public async Task<Definition> GetWebServiceDefinition(
            Type contract,
            string endpoint,
            string contractNamespace)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");

            if (!contract.IsInterface)
                throw new ArgumentException(
                    "An interface must be provided to generate a WSDL",
                    "contract");

            IEnumerable<Schema> schemas;

            var messages = new List<Message>();
            var portTypeOperations = new List<Models.PortType.Operation>();
            var bindingOperations = new List<Models.Binding.Operation>();

            using (var typeContext = _typeContextFactory.Create())
            {
                foreach (var method in contract.GetMethods())
                {
                    var methodDescription = typeContext
                        .GetDescriptionForMethod(method, contractNamespace);

                    // getting input message
                    var inputMessage = new Message
                    {
                        Name = string.Format("{0}_{1}_InputMessage", contract.Name, method.Name),
                        Parts = new List<MessagePart>
                        {
                            new ElementMessagePart(
                                "parameters", 
                                new QName(methodDescription.Input.Name, contractNamespace))
                        }
                    };

                    // getting output message
                    var outputMessage = new Message
                    {
                        Name = string.Format("{0}_{1}_OutputMessage", contract.Name, method.Name),
                        Parts = new List<MessagePart>
                        {
                            new ElementMessagePart(
                                "parameters", 
                                new QName(methodDescription.Output.Name, contractNamespace))
                        }
                    };

                    // getting operation for port type
                    var portTypeOperation = BuildOperationForPortType(
                        contract, 
                        contractNamespace, 
                        method.Name, 
                        inputMessage.Name, 
                        outputMessage.Name);

                    var bindingOperation = BuildOperationForBinding(
                        method, 
                        portTypeOperation);
                    
                    portTypeOperations.Add(portTypeOperation);
                    bindingOperations.Add(bindingOperation);

                    messages.Add(inputMessage);
                    messages.Add(outputMessage);
                }
                schemas = typeContext.GetSchemas();
            }

            var portType = new PortType
            {
                Name = contract.Name,
                Operations = portTypeOperations
            };

            // Currently only support basic HTTP binding
            var binding = new Binding
            {
                Name = string.Format("BasicHttpBinding_{0}", contract.Name),
                SoapBinding = new Models.Binding.SoapExtensions.Binding
                {
                    Style = Style.Document,
                    Transport = Transport.Http
                },
                Type = new QName(portType.Name, contractNamespace),
                Operations = bindingOperations
            };

            var service = new Service
            {
                Name = GetContractServiceName(contract.Name),
                Ports = new List<Port>
                {
                    new Port
                    {
                        Name = binding.Name,
                        Binding = new QName(binding.Name, contractNamespace),
                        Address = new Address
                        {
                            Location = endpoint
                        }
                    }
                }
            };

            var definition = new Definition
            {
                TargetNamespace = contractNamespace,
                QualifiedNamespaces = new List<QNamespace>
                {
                    new QNamespace("tns", contractNamespace)
                },
                Types = schemas,
                Messages = messages,
                PortTypes = new List<PortType> { portType },
                Bindings = new List<Binding> { binding },
                Services = new List<Service> { service }
            };

            return definition;
        }

        private static Models.Binding.RequestResponseOperation BuildOperationForBinding(
            MethodInfo method,
            RequestResponseOperation portTypeOperation)
        {
            var bindingOperation = new Models.Binding.RequestResponseOperation
            {
                Name = method.Name,
                SoapOperation = new Models.Binding.SoapExtensions.Operation
                {
                    SoapAction = portTypeOperation.Input.Action
                },
                Input = new Models.Binding.OperationMessage
                {
                    Body = new Models.Binding.SoapExtensions.Body
                    {
                        Use = Models.Binding.SoapExtensions.OperationMessageUse.Literal
                    }
                },
                Output = new Models.Binding.OperationMessage
                {
                    Body = new Models.Binding.SoapExtensions.Body
                    {
                        Use = Models.Binding.SoapExtensions.OperationMessageUse.Literal
                    }
                }
            };
            return bindingOperation;
        }

        private static RequestResponseOperation BuildOperationForPortType(
            Type contract, 
            string contractNamespace,
            string methodName, 
            string inputMessageName, 
            string outputMessageName)
        {
            var portTypeOperation = new RequestResponseOperation
            {
                Name = methodName,
                Input = new Models.PortType.OperationMessage
                {
                    Action = string.Format(
                        "{0}/{1}/{2}",
                        contractNamespace,
                        contract.Name,
                        methodName),
                    Message = new QName(inputMessageName, contractNamespace)
                },
                Output = new Models.PortType.OperationMessage
                {
                    Action = string.Format(
                        "{0}/{1}/{2}Response",
                        contractNamespace,
                        contract.Name,
                        methodName),
                    Message = new QName(outputMessageName, contractNamespace)
                }
            };
            return portTypeOperation;
        }

        private string GetContractServiceName(string contractName)
        {
            var serviceName = contractName;

            if (serviceName.StartsWith("I", StringComparison.InvariantCultureIgnoreCase))
                serviceName = serviceName.Remove(0, 1);

            return string.Format("{0}Service", serviceName);
        }
    }
}
