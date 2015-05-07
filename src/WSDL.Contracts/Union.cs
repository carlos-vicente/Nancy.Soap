using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class Union
    {
        public IEnumerable<QName> MemberTypes { get; set; }
    }
}