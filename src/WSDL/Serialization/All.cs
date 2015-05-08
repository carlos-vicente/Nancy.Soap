using System.Collections.Generic;

namespace WSDL.Serialization
{
    public class All : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}