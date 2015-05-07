using System.Xml;

namespace SOAP.Serialization
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
        public string Name { get; set; }

        public bool Abstract { get; set; }

        public SimpleContent SimpleContent { get; set; }

        public ComplexContent ComplexContent { get; set; }

        public ElementGrouping Grouping { get; set; }
        
        public override void WriteXml(XmlWriter writer)
        {
            
        }
    }
}