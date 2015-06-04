using System.Collections.Generic;

namespace WSDL.Models.Service
{
    public class Service
    {
        public string Name { get; set; }

        public IEnumerable<Port> Ports { get; set; }
    }
}