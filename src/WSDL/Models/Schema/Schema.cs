using System.Collections.Generic;

namespace WSDL.Models.Schema
{
    public class Schema
    {
        public string TargetNamespace { get; set; }

        public IEnumerable<QNamespace> QualifiedNamespaces { get; set; }

        public IEnumerable<Element> Elements { get; set; }

        public IEnumerable<SchemaType> Types { get; set; }

        public Schema()
        {
            QualifiedNamespaces = new List<QNamespace>();
            Elements = new List<Element>();
            Types = new List<SchemaType>();
        }
    }
}
