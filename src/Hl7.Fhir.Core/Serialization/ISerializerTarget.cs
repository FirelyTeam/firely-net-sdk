using System;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// General purpose serialization target, can be implemented in different ways to support serialization to custom formats
    /// </summary>
    public interface ISerializerTarget
    {
        /// <summary>
        /// Called at the beginning of an object (data type or resource).
        /// </summary>
        /// <param name="name">Name of the object-valued element - e.g. 'meta' or of its enclosing list - e.g. 'identifier'. It is null for root resources</param>
        /// <param name="resourceType">Resource type, null for data types</param>
        void BeginObject(string name, string resourceType);

        /// <summary>
        /// Called at the end of an object
        /// </summary>
        void EndObject(string name, string resourceType);

        /// <summary>
        /// Called at the beginning of a list
        /// </summary>
        /// <param name="name">Name of the list element - e.g. 'identifier'</param>
        void BeginList(string name);

        /// <summary>
        /// Called at the end of a list
        /// </summary>
        void EndList(string name);

        /// <summary>
        /// Writes the value of string-based primitive types (FhirString but also FhirCode, FhirDateTime etc.)
        /// </summary>
        void WriteValue(string stringValue);

        /// <summary>
        /// Writes the value of boolean-based primitive types (FhirBoolean)
        /// </summary>
        void WriteValue(bool boolValue);

        /// <summary>
        /// Writes the value of integer-based primitive types (Integer, PositiveInt etc.)
        /// </summary>
        void WriteValue(int intValue);

        /// <summary>
        /// Writes the value of decimal-based primitive types (FhirDecimal)
        /// </summary>
        void WriteValue(decimal decimalValue);

        /// <summary>
        /// Writes the value of DateTimeOffset-based primitive types (Instant)
        /// </summary>
        void WriteValue(DateTimeOffset dateTimeOffsetValue);

        /// <summary>
        /// Writes the value of byte array-based primitive types (Base64Binary)
        /// </summary>
        void WriteValue(byte[] bytesValue);

        /// <summary>
        /// Writes the value of an attribute (element id and extension url)
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Attribute value</param>
        void WriteAttribute(string name, string value);

        /// <summary>
        /// Writes the value of XHTML-valued elements ('div')
        /// </summary>
        /// <param name="name">Elment name</param>
        /// <param name="value">Element value</param>
        void WriteXhtmlElement(string name, string value);
    }
}
