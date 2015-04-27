using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSDL.Contracts;
using Type = WSDL.Contracts.Type;

namespace WSDL.Gen
{
    public class WsdlGenerator : IWsdlGenerator
    {
        #region primitives
        private static readonly Schema PrimitiveTypesSchema = new Schema
        {
            TargetNamespace = "http://schemas.microsoft.com/2003/10/Serialization/",
            Types = new List<SchemaType>
            {
                new SimpleType("char", new Restriction
                {
                    Base = "int"
                }),
                new SimpleType("duration", new Restriction
                {
                    Base = "duration",
                    Pattern = @"\-?P(\d*D)?(T(\d*H)?(\d*M)?(\d*(\.\d*)?S)?)?",
                    MinimumInclusive = "-P10675199DT2H48M5.4775808S",
                    MaximumInclusive = "P10675199DT2H48M5.4775807S"
                }),
                new SimpleType("guid", new Restriction
                {
                    Base = "string",
                    Pattern = @"[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12}"
                })
            },
            Elements = new List<Element>
            {
                new Element { Name = "anyType", Nillable = true, Type = new Type { Name ="anyType" }},
                new Element { Name = "anyURI", Nillable = true, Type = new Type { Name ="anyURI" }},
                new Element { Name = "base64Binary", Nillable = true, Type = new Type { Name ="base64Binary" }},
                new Element { Name = "boolean", Nillable = true, Type = new Type { Name ="boolean" }},
                new Element { Name = "byte", Nillable = true, Type = new Type { Name ="byte" }},
                new Element { Name = "dateTime", Nillable = true, Type = new Type { Name ="dateTime" }},
                new Element { Name = "decimal", Nillable = true, Type = new Type { Name ="decimal" }},
                new Element { Name = "double", Nillable = true, Type = new Type { Name ="double" }},
                new Element { Name = "float", Nillable = true, Type = new Type { Name ="float" }},
                new Element { Name = "int", Nillable = true, Type = new Type { Name ="int" }},
                new Element { Name = "long", Nillable = true, Type = new Type { Name ="long" }},
                new Element { Name = "QName", Nillable = true, Type = new Type { Name ="QName" }},
                new Element { Name = "short", Nillable = true, Type = new Type { Name ="short" }},
                new Element { Name = "string", Nillable = true, Type = new Type { Name ="string" }},
                new Element { Name = "unsignedByte", Nillable = true, Type = new Type { Name ="unsignedByte" }},
                new Element { Name = "unsignedInt", Nillable = true, Type = new Type { Name ="unsignedInt" }},
                new Element { Name = "unsignedLong", Nillable = true, Type = new Type { Name ="unsignedLong" }},
                new Element { Name = "unsignedShort", Nillable = true, Type = new Type { Name ="unsignedShort" }},
                new Element { Name = "char", Nillable = true, Type = new Type { Name ="char" }}
            }
        };
        #endregion

        public async Task<Definition> GetWebServiceDefinition(System.Type contract)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");

            if (!contract.IsInterface)
                throw new ArgumentException(
                    "An interface must be provided to generate a WSDL",
                    "contract");

            var definition = new Definition
            {
                TargetNamespace = "http://carlos.vicente.org"
            };
            definition.Types.Add(PrimitiveTypesSchema);

            return definition;
        }
    }
}
