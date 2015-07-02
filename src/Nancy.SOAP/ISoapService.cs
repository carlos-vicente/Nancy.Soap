using System.Threading.Tasks;
using WSDL.Serialization;

namespace Nancy.SOAP
{
    public interface ISoapService<T> where T : class
    {
        Task<Definition> GetContractDefinition(string endpoint);
        Task InvokeContractMethod(); //Soap response for soap request
    }
}