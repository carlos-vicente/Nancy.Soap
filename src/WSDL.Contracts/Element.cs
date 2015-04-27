namespace WSDL.Contracts
{
    public class Element
    {
        public string Name { get; set; }

        public bool Nillable { get; set; }

        public Type Type { get; set; }

        public int MinimumOccurrences { get; set; }

        public int MaximumOccurrences { get; set; }
    }
}