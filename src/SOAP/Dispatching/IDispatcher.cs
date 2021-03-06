﻿using System.Collections.Generic;
using System.Reflection;

namespace SOAP.Dispatching
{
    public interface IDispatcher<in T> where T : class
    {
        void InvokeMethod(
            T instance, 
            MethodInfo methodInfo,
            IDictionary<string, object> parameters);
    }
}
