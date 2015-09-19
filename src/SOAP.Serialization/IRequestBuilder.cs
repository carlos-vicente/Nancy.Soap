using System.Collections.Generic;
using SOAP.Models;

namespace SOAP.Serialization
{
    public interface IRequestBuilder
    {
        Request BuildRequestFor(
            string operationName, 
            IDictionary<string, string> parameters);
    }
}