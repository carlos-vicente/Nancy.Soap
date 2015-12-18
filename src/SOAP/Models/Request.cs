using System.Collections.Generic;

namespace SOAP.Models
{
    public class Request
    {
        public string MethodName { get; set; }

        public IDictionary<string, string> Parameters { get; set; }
    }
}