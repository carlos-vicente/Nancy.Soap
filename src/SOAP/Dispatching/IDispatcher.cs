using System.Threading.Tasks;
using SOAP.Models;

namespace SOAP.Dispatching
{
    public interface IDispatcher
    {
        Task<Response> InvokeMethod(Request request);
    }
}
