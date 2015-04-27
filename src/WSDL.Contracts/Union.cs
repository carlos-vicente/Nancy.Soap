using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class Union
    {
        public string Id { get; set; }

        public IEnumerable<string> MemberTypes { get; set; }
    }
}