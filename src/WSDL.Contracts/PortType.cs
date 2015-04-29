using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class PortType
    {
        public string Name { get; set; }

        public IEnumerable<Operation> Operations { get; set; }
    }
}