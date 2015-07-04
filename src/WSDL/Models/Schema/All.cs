using System.Collections.Generic;

namespace WSDL.Models.Schema
{
    public class All : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}