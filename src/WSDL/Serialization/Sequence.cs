using System.Collections.Generic;

namespace WSDL.Serialization
{
    public class Sequence : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}