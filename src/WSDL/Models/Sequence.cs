using System.Collections.Generic;

namespace WSDL.Models
{
    public class Sequence : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}