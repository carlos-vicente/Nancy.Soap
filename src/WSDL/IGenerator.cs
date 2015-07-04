using System;
using System.Threading.Tasks;
using WSDL.Models;

namespace WSDL
{
    public interface IGenerator
    {
        Task<Definition> GetWebServiceDefinition(Type contract, string endpoint);
        Task<Definition> GetWebServiceDefinition(Type contract, string endpoint, string contractNamespace);
    }
}