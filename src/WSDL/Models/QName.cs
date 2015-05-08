namespace WSDL.Models
{
    public class QName
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public QName(string name)
        {
            Name = name;
        }

        public QName(string name, string ns)
            : this(name)
        {
            Namespace = ns;
        }
    }
}