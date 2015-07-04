using System;
using System.Threading.Tasks;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaskService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TaskService.svc or TaskService.svc.cs at the Solution Explorer and start debugging.
    public class TaskService : ITaskService
    {
        public Task<Response> DoWork()
        {
            throw new NotImplementedException();
        }
    }
}
