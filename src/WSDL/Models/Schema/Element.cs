namespace WSDL.Models.Schema
{
    public class Element
    {
        public string Name { get; set; }

        public bool Nillable { get; set; }

        public QName Type { get; set; }

        public int? MinimumOccurrences { get; set; }

        public int? MaximumOccurrences { get; set; }
    }
}