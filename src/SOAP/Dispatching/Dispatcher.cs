using System;
using System.Reflection;
using System.Threading.Tasks;
using SOAP.Models;

namespace SOAP.Dispatching
{
    public class Dispatcher<T> : IDispatcher
        where T : class
    {
        private readonly T _instance;

        public Dispatcher(T instance)
        {
            _instance = instance;
        }

        public async Task<Response> InvokeMethod(Request request)
        {
            // DON?T DO TASK.RUN (MAINLY IN A LIBRARY)

            //return await Task.Run(() =>
            //{
            //    var type = typeof (T);

            //    var method = type.GetMethod(
            //        request.Name,
            //        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);

            //    if (method == null)
            //        throw new InvalidOperationException();

            //    var response = method.Invoke(_instance, request.Parameters);

            //    return new Response
            //    {
            //        Name = string.Format("{0}Response", method.Name),
            //        Content = response
            //    };
            //});

            throw new NotImplementedException();
        }
    }
}