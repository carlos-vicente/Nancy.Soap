namespace SOAP.Serialization
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
        public Restriction Restriction { get; set; }
        public Extension Extension { get; set; }
    }
}