using System.Threading.Tasks;
using WSDL.Serialization;

namespace Nancy.SOAP
{
    public interface ISoapService<T>
    {
        Task<Definition> GetContractDefinition(string endpoint);
        //Task<global::SOAP.Serialization.Response> InvokeContractMethod(global::SOAP.Serialization.Request request);
    }
}