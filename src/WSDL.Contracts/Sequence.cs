using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class Sequence : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}