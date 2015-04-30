namespace WSDL.Contracts
{
    public class QNamespace
    {
        public string Abbreviation { get; private set; }
        public string Namespace { get; private set; }

        public QNamespace(string abbreviation, string ns)
        {
            Abbreviation = abbreviation;
            Namespace = ns;
        }
    }
}