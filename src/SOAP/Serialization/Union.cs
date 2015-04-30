using System.Collections.Generic;

namespace SOAP.Serialization
{
    public class Union
    {
        public string Id { get; set; }

        public IEnumerable<string> MemberTypes { get; set; }
    }
}