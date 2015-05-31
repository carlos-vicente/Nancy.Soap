using System;

namespace WSDL.Models
{
    /// <summary>
    /// An base class that defines any type that can be used within a Schema.
    /// It can only be either a SimpleType or a ComplexType.
    /// </summary>
    public abstract class SchemaType : IEquatable<SchemaType>
    {
        public string Name { get; protected set; }
        
        public bool Equals(SchemaType other)
        {
            return Name.Equals(
                other.Name, 
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}