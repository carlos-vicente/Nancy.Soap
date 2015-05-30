﻿namespace WSDL.Models
{
    /// <summary>
    /// An base class that defines any type that can be used within a Schema.
    /// It can only be either a SimpleType or a ComplexType.
    /// </summary>
    public abstract class SchemaType
    {
        public string Name { get; protected set; }
    }
}