using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WSDL.Models;
using WSDL.Models.Schema;

namespace WSDL.TypeManagement
{
    public class TypeContext : ITypeContext
    {
        private readonly IDictionary<string, ICollection<SchemaType>> _types;

        public IPrimitiveTypeProvider PrimitiveTypeProvider { get; private set; }
        
        public TypeContext(IPrimitiveTypeProvider primitiveTypeProvider)
        {
            PrimitiveTypeProvider = primitiveTypeProvider;

            _types = new Dictionary<string, ICollection<SchemaType>>();
        }

        public MethodDescription GetDescriptionForMethod(
            MethodInfo methodInfo, 
            string contractNamespace)
        {
            return new MethodDescription
            {
                Input = GetInputTypeFor(methodInfo, contractNamespace),
                Output = GetOutputTypeFor(methodInfo, contractNamespace)
            };
        }

        public IEnumerable<Schema> GetSchemas()
        {
            var schemas = new ConcurrentBag<Schema>
            {
                PrimitiveTypeProvider.GetPrimitiveTypesSchema()
            };
            
            foreach (var targetNamespace in _types.Keys.AsParallel())
            {
                schemas.Add(new Schema
                {
                    TargetNamespace = targetNamespace,
                    Types = _types[targetNamespace],
                    Elements = _types[targetNamespace].Select(t => new Element
                    {
                        Name = t.Name,
                        Type = new QName(t.Name, targetNamespace)
                    })
                });
            }

            return schemas;
        }

        public void Dispose()
        {
            _types.Clear();
        }

        private ComplexType GetInputTypeFor(MethodInfo methodInfo, string contractNamespace)
        {
            // Type.IsPrimitive:
            // The primitive types are Boolean, Byte, SByte, Int16, UInt16, 
            // Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
            // If the current Type represents a generic type, or a type parameter in the 
            // definition of a generic type or generic method, this property always returns false.

            var inputSequence = new Sequence
            {
                Elements = (from parameter in methodInfo.GetParameters()
                            let typeName = PrimitiveTypeProvider
                                .GetQNameForType(parameter.ParameterType)
                            where typeName != null
                            select new Element
                            {
                                Name = parameter.Name,
                                Type = typeName,
                                Nillable = !parameter.ParameterType.IsPrimitive
                            })
                        .ToList()
            };

            var inputType = new ComplexType(
                methodInfo.Name,
                inputSequence);

            Add(contractNamespace, inputType);

            return inputType;
        }

        private ComplexType GetOutputTypeFor(MethodInfo methodInfo, string contractNamespace)
        {
            var outputElements = new List<Element>();

            if (methodInfo.ReturnType != typeof(void))
                outputElements.Add(
                    new Element
                    {
                        Name = string.Format("{0}Result", methodInfo.Name),
                        Type = PrimitiveTypeProvider
                            .GetQNameForType(methodInfo.ReturnType),
                        Nillable = !methodInfo.ReturnType.IsPrimitive
                    });

            var outputType = new ComplexType(
                string.Format("{0}Response", methodInfo.Name),
                new Sequence
                {
                    Elements = outputElements
                });

            Add(contractNamespace, outputType);

            return outputType;
        }

        private void Add(string contractNamespace, SchemaType inputType)
        {
            if (_types.ContainsKey(contractNamespace))
            {
                if (!_types[contractNamespace].Contains(inputType))
                    _types[contractNamespace].Add(inputType);
            }
            else
            {
                _types.Add(contractNamespace, new List<SchemaType>
                {
                    inputType
                });
            }
        }
    }
}