using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [DataContract]
    public class Response
    {
        public int ReturnCode { get; set; }
    }

    [ServiceContract]
    public interface ITaskService
    {
        [OperationContract]
        Task<Response> DoWork();
    }
}
