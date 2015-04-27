using System.Threading.Tasks;
using WSDL.Contracts;
using Type = System.Type;

namespace WSDL.Gen
{
    public interface IWsdlGenerator
    {
        Task<Definition> GetWebServiceDefinition(Type contract);
    }
}