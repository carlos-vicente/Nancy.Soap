using System.Threading.Tasks;
using WSDL.Serialization;
using SOAP.Serialization;

namespace SOAP.Service
{
    public interface ISoapService<T>
    {
        Task<Definition> GetContractDefinition(string endpoint);

        Task<Envelope> InvokeContractMethod(
            string soapAction,
            Envelope request);
    }
}