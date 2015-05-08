using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WSDL.Serialization
{
    /// <summary>
    /// The constraint that should be applied to a given data type in order to define a simple type
    /// </summary>
    public class Restriction : IXmlSerializable
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader) { throw new System.NotImplementedException(); }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("restriction", Schema.XmlSchemaNamespace);

            string prefix = null;
            if(this.Base.Namespace != null)
                prefix = writer.LookupPrefix(this.Base.Namespace);

            writer.WriteAttributeString(
                "base",
                prefix == null
                    ? this.Base.Name
                    : string.Format("{0}:{1}", prefix, this.Base.Name));

            if (MinimumInclusive != null)
                WriteElementWithValue(writer, "minInclusive", MinimumInclusive);

            if (MaximumInclusive != null)
                WriteElementWithValue(writer, "maxInclusive", MaximumInclusive);

            if (MinimumExclusive != null)
                WriteElementWithValue(writer, "minExclusive", MinimumExclusive);

            if (MaximumExclusive != null)
                WriteElementWithValue(writer, "maxExclusive", MaximumExclusive);

            if (FractionDigits.HasValue)
                WriteElementWithValue(writer, "fractionDigits", FractionDigits.Value.ToString());

            if (Enumerations != null && Enumerations.Any())
            {
                foreach (var enumValue in Enumerations)
                {
                    WriteElementWithValue(writer, "enumeration", enumValue);
                }
            }

            if (Pattern != null)
                WriteElementWithValue(writer, "pattern", Pattern);

            if (WhiteSpace.HasValue)
                WriteElementWithValue(writer, "whiteSpace", WhiteSpace.Value.ToString());

            if (Length.HasValue)
                WriteElementWithValue(writer, "length", Length.Value.ToString());

            if (MinimumLength.HasValue)
                WriteElementWithValue(writer, "minLength", MinimumLength.Value.ToString());

            if (MaximumLength.HasValue)
                WriteElementWithValue(writer, "maxLength", MaximumLength.Value.ToString());

            if (TotalDigits.HasValue)
                WriteElementWithValue(writer, "totalDigits", TotalDigits.Value.ToString());

            writer.WriteEndElement();
        }

        private void WriteElementWithValue(XmlWriter writer, string elementName, string value)
        {
            writer.WriteStartElement(elementName, Schema.XmlSchemaNamespace);
            writer.WriteAttributeString("value", value);
            writer.WriteEndElement();
        }
    }
}