using System;
using System.Threading.Tasks;
using WSDL.Contracts;

namespace WSDL.Gen
{
    public interface IWsdlGenerator
    {
        Task<Definition> GetWebServiceDefinition(Type contract);
    }
}