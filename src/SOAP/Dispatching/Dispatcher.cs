using System.Collections.Generic;
using System.Reflection;

namespace SOAP.Dispatching
{
    public class Dispatcher<T> : IDispatcher<T> where T : class
    {
        public void InvokeMethod(T instance, MethodInfo methodInfo, IDictionary<string, object> parameters)
        {
            
        }
    }
}