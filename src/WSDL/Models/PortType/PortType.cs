using System.Collections.Generic;

namespace WSDL.Models.PortType
{
    public class PortType
    {
        public string Name { get; set; }

        public IEnumerable<Operation> Operations { get; set; }
    }
}