using System.Collections.Generic;

namespace WSDL.Models.Binding
{
    public class Binding
    {
        public string Name { get; set; }

        public QName Type { get; set; }

        public SoapExtensions.Binding SoapBinding { get; set; }

        public IEnumerable<Operation> Operations { get; set; }
    }
}