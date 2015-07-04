using System.Collections.Generic;

namespace WSDL.Models.Schema
{
    /// <summary>
    /// The constraint that should be applied to a given data type in order to define a simple type
    /// </summary>
    public class Restriction
    {
        /// <summary>
        /// The primitive data type to apply the restriction
        /// </summary>
        public QName Base { get; set; }

        /// <summary>
        /// Specifies the lower bounds for numeric values (the value must be greater than or equal to this value)
        /// </summary>
        public string MinimumInclusive { get; set; }

        /// <summary>
        /// Specifies the upper bounds for numeric values (the value must be less than or equal to this value)
        /// </summary>
        public string MaximumInclusive { get; set; }

        /// <summary>
        /// Specifies the lower bounds for numeric values (the value must be greater than this value)
        /// </summary>
        public string MinimumExclusive { get; set; }

        /// <summary>
        /// Specifies the upper bounds for numeric values (the value must be less than this value)
        /// </summary>
        public string MaximumExclusive { get; set; }

        /// <summary>
        /// Specifies the maximum number of decimal places allowed. Must be equal to or greater than zero
        /// </summary>
        public int? FractionDigits { get; set; }

        /// <summary>
        /// Defines a list of acceptable values
        /// </summary>
        public IEnumerable<string> Enumerations { get; set; }

        /// <summary>
        /// Defines the exact sequence of characters that are acceptable
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Specifies how white space (line feeds, tabs, spaces, and carriage returns) is handled
        /// </summary>
        public WhiteSpaceConstraint? WhiteSpace { get; set; }

        /// <summary>
        /// Specifies the exact number of characters or list items allowed. Must be equal to or greater than zero
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// Specifies the minimum number of characters or list items allowed. Must be equal to or greater than zero
        /// </summary>
        public int? MinimumLength { get; set; }

        /// <summary>
        /// Specifies the maximum number of characters or list items allowed. Must be equal to or greater than zero
        /// </summary>
        public int? MaximumLength { get; set; }

        /// <summary>
        /// Specifies the exact number of digits allowed. Must be greater than zero
        /// </summary>
        public int? TotalDigits { get; set; }
    }
}