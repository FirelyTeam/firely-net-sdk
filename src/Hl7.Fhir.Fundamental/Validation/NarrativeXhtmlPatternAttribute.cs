/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Validates an xhtml value against the FHIR rules for xhtml.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NarrativeXhtmlPatternAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
            IsValid(value, validationContext.GetNarrativeValidationKind(), validationContext);

        /// <summary>
        /// Validates whether the value is a string of well-formatted Xml.
        /// </summary>
        public ValidationResult? IsValid(object? value, NarrativeValidationKind kind, ValidationContext context)
        {
            if (value is null) return ValidationResult.Success;

            if (value is string xml)
            {
                return kind switch
                {
                    NarrativeValidationKind.None => ValidationResult.Success,
                    NarrativeValidationKind.Xml => XHtml.IsValidXml(xml, out var error)
                            ? ValidationResult.Success
                            : COVE.NARRATIVE_XML_IS_MALFORMED.AsResult(context, error),
                    NarrativeValidationKind.FhirXhtml => runValidateXhtmlSchema(xml, context),
                    _ => throw new NotSupportedException($"Encountered unknown narrative validation kind {kind}.")
                };
            }
            else
                throw new ArgumentException($"{nameof(NarrativeXhtmlPatternAttribute)} attributes can only be applied to string properties.");
        }

        private static ValidationResult runValidateXhtmlSchema(string text, ValidationContext context)
        {
            try
            {
                var doc = SerializationUtil.XDocumentFromXmlText(text);

#if NETSTANDARD1_6
                var errors = new string[0];
#else
                var errors = SerializationUtil.RunFhirXhtmlSchemaValidation(doc);
#endif
                return errors.Any() ? COVE.NARRATIVE_XML_IS_INVALID.AsResult(context, string.Join(", ", errors)) : ValidationResult.Success!;
            }
            catch (FormatException fe)
            {
                return COVE.NARRATIVE_XML_IS_MALFORMED.AsResult(context, fe.Message);
            }
        }
    }
}



#nullable restore