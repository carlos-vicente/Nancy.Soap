namespace WSDL.Contracts
{
    /// <summary>
    /// An xml simple type. 
    /// It can be a constraint on some other simple type (use property Restriction).
    /// It can be a list of some simple type (use property List).
    /// It can be the union of several simple types (use property Union).
    /// </summary>
    public class SimpleType : SchemaType
    {
        /// <summary>
        /// The simple type name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Used when defining a constraint on some other simple type
        /// When using Restriction, can't use List nor Union
        /// </summary>
        public Restriction Restriction { get; private set; }
        
        /// <summary>
        /// Used when defining a list of some simple type
        /// When using List, can't use Restriction nor Union
        /// </summary>
        public List List { get; private set; }

        /// <summary>
        /// Used when defining the union of several simple types
        /// When using Union, can't use Restriction nor List
        /// </summary>
        public Union Union { get; private set; }

        private SimpleType(string name)
        {
            Name = name;
        }

        public SimpleType(string nameOfConstraint, Restriction constraint)
            : this(nameOfConstraint)
        {
            Restriction = constraint;
        }

        public SimpleType(string nameOfList, List listOfType)
            : this(nameOfList)
        {
            List = listOfType;
        }

        public SimpleType(string nameOfUnion, Union membersUnion)
            : this(nameOfUnion)
        {
            Union = membersUnion;
        }
    }
}