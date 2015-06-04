using System.Collections.Generic;

namespace WSDL.Models.Schema
{
    public class Sequence : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
    }
}