namespace WSDL.Models
{
    // <complexContent
    // id=ID
    // mixed=true|false
    // any attributes >
    // 
    // (annotation?,(restriction|extension))
    // 
    // </complexContent>
    public class ComplexContent
    {
        public Restriction Restriction { get; private set; }
        public Extension Extension { get; private set; }

        public ComplexContent(Restriction restriction)
        {
            Restriction = restriction;
        }

        public ComplexContent(Extension extension)
        {
            Extension = extension;
        }
    }
}