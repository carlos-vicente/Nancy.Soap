using WSDL.Models;

namespace WSDL.TypeManagement
{
    /// <summary>
    /// Holds the input and output complex types for a given method
    /// </summary>
    public class MethodDescription
    {
        /// <summary>
        /// ComplexType for the input parameters
        /// </summary>
        public ComplexType Input { get; set; }

        /// <summary>
        /// SchemaType for the output parameter, it can either be 
        /// an SimpleType or a ComplexType
        /// </summary>
        public SchemaType Output { get; set; }
    }
}