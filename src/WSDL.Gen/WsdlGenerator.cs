using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSDL.Contracts;

namespace WSDL.Gen
{
    public class WsdlGenerator
    {
        private static readonly Schema PrimitiveTypesSchema = new Schema
        {
            Types = new List<SchemaType>
            {
                new SimpleType()
            }
        };

        public async Task<Definition> GetWebServiceDefinition(Type contract)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");

            if (!contract.IsInterface)
                throw new ArgumentException(
                    "An interface must be provided to generate a WSDL",
                    "contract");

            throw new NotImplementedException();
        } 
    }
}
