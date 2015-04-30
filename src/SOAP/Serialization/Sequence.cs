using System.Collections.Generic;

namespace SOAP.Serialization
{
    public class Sequence : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}