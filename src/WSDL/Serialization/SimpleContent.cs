namespace WSDL.Serialization
{
    // <simpleContent
    // id=ID
    // any attributes >
    // 
    // (annotation?,(restriction|extension))
    // 
    // </simpleContent>
    public class SimpleContent
    {
        public Restriction Restriction { get; set; }
        public Extension Extension { get; set; }
    }
}