#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlPocoDeserializerSettings
    {
        /// <summary>
        /// If the caller will not access base64 data in the deserialized resources, base64 decoding
        /// of <see cref="Base64Binary"/> values can be turned off to increase performance.
        /// </summary>
        /// <remarks>The <see cref="Base64Binary" /> element's <see cref="PrimitiveType.ObjectValue" /> will
        /// still contain the unparsed base64 data and will therefore be retained and round-tripped.</remarks>
        public bool DisableBase64Decoding { get; init; } = false;

        /// <summary>
        /// If set, this validator is invoked before the value is set in the object under construction to validate
        /// and possibly alter the value. Setting this property to <c>null</c> will disable validation completely.
        /// </summary>
        public IDeserializationValidator? Validator { get; init; } = DataAnnotationDeserialzationValidator.Default;

        /// <summary>
        /// Perform the validation on the deserialized object even if parsing issues occurred
        /// </summary>
        /// <remarks>
        /// This is useful for "strict mode" once pass validators
        /// </remarks>
        public bool ValidateOnFailedParse { get; init; } = false;
    }
}

#nullable restore
