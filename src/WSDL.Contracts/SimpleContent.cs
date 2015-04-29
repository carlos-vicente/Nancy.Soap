namespace WSDL.Contracts
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
        public Restriction Restriction { get; private set; }
        public Extension Extension { get; private set; }

        public SimpleContent(Restriction restriction)
        {
            Restriction = restriction;
        }

        public SimpleContent(Extension extension)
        {
            Extension = extension;
        }
    }
}