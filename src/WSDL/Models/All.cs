using System.Collections.Generic;

namespace WSDL.Models
{
    public class All : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}