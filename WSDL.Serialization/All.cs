using System.Collections.Generic;
using System.Xml;

namespace WSDL.Serialization
{
    public class All : ElementGrouping
    {
        public IEnumerable<Element> Elements { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            
        }
    }
}