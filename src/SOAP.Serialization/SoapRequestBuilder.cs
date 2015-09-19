using System;
using System.Collections.Generic;
using SOAP.Models;

namespace SOAP.Serialization
{
    public class SoapRequestBuilder : IRequestBuilder
    {
        public Request BuildRequestFor(
            string operationName, 
            IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}