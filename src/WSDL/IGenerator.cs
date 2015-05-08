using System;
using System.Threading.Tasks;
using WSDL.Models;

namespace WSDL
{
    public interface IGenerator
    {
        Task<Definition> GetWebServiceDefinition(Type contract);
    }
}