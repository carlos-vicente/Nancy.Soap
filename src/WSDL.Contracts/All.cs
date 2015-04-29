using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class All : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}