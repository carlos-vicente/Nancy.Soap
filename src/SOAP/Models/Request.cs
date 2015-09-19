using System.Collections.Generic;

namespace SOAP.Models
{
    public class Request
    {
        public string Name { get; set; }

        public IDictionary<string, object> Parameters { get; set; }
    }
}