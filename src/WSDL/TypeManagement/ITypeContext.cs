using System;
using System.Collections.Generic;
using System.Reflection;
using WSDL.Models;

namespace WSDL.TypeManagement
{
    /// <summary>
    /// This provides a stateful context for managing the types required to define
    /// a web service. It includes all the types, both static and instance related
    /// </summary>
    public interface ITypeContext : IDisposable
    {
        /// <summary>
        /// It transforms the parameter types and return types into complex types for both
        /// input and output
        /// </summary>
        /// <param name="methodInfo">The method to transform</param>
        /// <param name="contractNamespace">The namespace of the contract to which this method belongs to</param>
        /// <returns>Method description with input and output types</returns>
        MethodDescription GetDescriptionForMethod(MethodInfo methodInfo, string contractNamespace);
        
        /// <summary>
        /// Returns the schemas for the types built during the last execution
        /// </summary>
        /// <returns>The schemas containing all the types built</returns>
        IEnumerable<Schema> GetSchemas();
    }
}
