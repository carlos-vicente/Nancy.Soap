using System;
using System.Threading.Tasks;
using WSDL.Contracts;

namespace WSDL.Gen
{
    public class WsdlGenerator
    {
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
