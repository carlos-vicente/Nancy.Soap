using System.Collections.Generic;

namespace WSDL.Models.Schema
{
    public class Union
    {
        public IEnumerable<QName> MemberTypes { get; set; }
    }
}