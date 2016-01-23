using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using SOAP.Models;

namespace SOAP.Dispatching
{
    public class Dispatcher<T> : IDispatcher where T : class
    {
        private readonly T _instance;

        public Dispatcher(T instance)
        {
            _instance = instance;
        }

        public Task<Response> InvokeMethod(Request request)
        {
            var type = typeof(T);

            var method = type
                .GetMethod(request.MethodName);

            if (method == null)
                throw new InvalidOperationException();

            var parameters = BuildParameters(method, request.Parameters);

            var response = method.Invoke(_instance, parameters);

            return Task.FromResult(new Response
            {
                Name = string.Format("{0}Response", method.Name),
                Content = response
            });
        }

        private object[] BuildParameters(
            MethodInfo method,
            IDictionary<string, string> parameters)
        {
            var builtParameters = new List<object>();

            foreach (var parameterInfo in method.GetParameters())
            {
                if (parameters.ContainsKey(parameterInfo.Name))
                {
                    builtParameters
                        .Add(GetTypedParameter(
                            parameterInfo.ParameterType,
                            parameters[parameterInfo.Name]));
                }
                else
                {
                    // adding the default value for a non existing parameter
                    // which is possible if contract defined with minOccurs=0
                    builtParameters.Add(
                        parameterInfo.ParameterType.IsValueType
                            ? Activator.CreateInstance(parameterInfo.ParameterType)
                            : null);
                }
                
            }

            return builtParameters.ToArray();
        }

        private object GetTypedParameter(Type parameterType, string value)
        {
            // TODO: support complex types, if we have still have xml on the value, 
            // then it should be read on to an object
            return Convert.ChangeType(value, parameterType);
        }
    }
}