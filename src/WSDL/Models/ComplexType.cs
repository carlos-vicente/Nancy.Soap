namespace WSDL.Models
{
    // <complexType
    // id=ID
    // name=NCName
    // abstract=true|false
    // mixed=true|false
    // block=(#all|list of (extension|restriction))
    // final=(#all|list of (extension|restriction))
    // any attributes
    // >
    // 
    // (annotation?,(simpleContent|complexContent|((group|all|choice|sequence)?,((attribute|attributeGroup)*,anyAttribute?))))
    // 
    // </complexType>
    public class ComplexType : SchemaType
    {
        public string Name { get; private set; }

        public bool Abstract { get; private set; }

        public SimpleContent SimpleContent { get; private set; }

        public ComplexContent ComplexContent { get; private set; }

        public ElementGrouping Grouping { get; set; }

        private ComplexType(string name, bool abstractType)
        {
            Name = name;
            Abstract = abstractType;
        }

        public ComplexType(string name, SimpleContent simpleContent, bool abstractType = false)
            : this(name, abstractType)
        {
            SimpleContent = simpleContent;
        }

        public ComplexType(string name, ComplexContent complexContent, bool abstractType = false)
            : this(name, abstractType)
        {
            ComplexContent = complexContent;
        }

        public ComplexType(string name, ElementGrouping grouping, bool abstractType = false)
            : this(name, abstractType)
        {
            Grouping = grouping;
        }
    }
}