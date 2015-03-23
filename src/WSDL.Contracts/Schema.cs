using System.Collections.Generic;

namespace WSDL.Contracts
{
    public class Schema
    {
        public IEnumerable<Element> Elements { get; set; }

        public IEnumerable<SchemaType> Types { get; set; }

        public Schema()
        {
            Elements = new List<Element>();
            Types = new List<SchemaType>();
        }
    }
}
