#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Hl7.FhirPath.Sprache;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlPocoDeserializerSettings
    {
        /// <summary>
        /// If set, this validator is invoked before the value is set in the object under construction to validate
        /// and possibly alter the value. Setting this property to <c>null</c> will disable validation completely.
        /// </summary>
        public IDeserializationValidator? Validator { get; init; } = DataAnnotationDeserialzationValidator.Default;

        /// <summary>
        /// Perform the parse time validation on the deserialized object even if parsing issues occurred.
        /// </summary>
        /// <remarks>
        /// This is useful for "strict mode" once pass validators and may result in spurious error messages
        /// from validating incomplete content.
        /// </remarks>
        public bool ValidateOnFailedParse { get; init; } = false;

        /// <summary>
        /// During parsing any contained resources (such as those in a bundle) that encounter some form of parse/validation exception
        /// will have a <c>List&lt;CodedException&gt;</c> of these exceptions added as an annotation to the child resource.
        /// </summary>
        /// <remarks>
        /// This is primarily added to ease the processing of bundles during a batch submission.
        /// (without requiring processing fhirpath expressions in the issues in the parsing operation outcome to determine if a 
        /// resource was clean and possibly ok to process).
        /// </remarks>
        public bool AnnotateResourceParseExceptions { get; init; } = false;


        /// <summary>
        /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
        /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
        /// default. Set this property to any other value than <see cref="None{T}"/>
        /// to perform validation.
        /// </summary>
        public NarrativeValidationKind NarrativeValidation { get; } = NarrativeValidationKind.None;
    }
}

#nullable restore