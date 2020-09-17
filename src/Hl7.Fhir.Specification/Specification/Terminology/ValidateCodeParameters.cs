/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public class ValidateCodeParameters
    {
        /// <summary>
        /// A canonical reference to a value set.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The context of the value set, so that the server can resolve this to a value set to validate against.
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// The value set is provided directly as part of the request.
        /// </summary>
        public Resource ValueSet { get; set; }
        /// <summary>
        /// The identifier that is used to identify a specific version of the value set to be used when validating the code.
        /// </summary>
        public string ValueSetVersion { get; set; }
        /// <summary>
        /// The code that is to be validated.
        /// </summary>
        /// <remarks>If a code is provided, a system or a context must be provided.</remarks>
        public string Code { get; set; }
        /// <summary>
        /// The system for the code that is to be validated
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// The version of the system.
        /// </summary>
        public string SystemVersion { get; set; }
        /// <summary>
        /// The display associated with the code.
        /// </summary>
        /// <remarks>If a display is provided a code must be provided.</remarks>
        public string Display { get; set; }
        /// <summary>
        /// A coding to validate.
        /// </summary>
        public Coding Coding { get; set; }
        /// <summary>
        /// A full codeableConcept to validate.
        /// </summary>
        /// <remarks>The server returns true if one of the coding values is in the value set, and may also validate that the codings are not in conflict with each other if more than one is present.</remarks>
        public CodeableConcept CodeableConcept { get; set; }
        /// <summary>
        /// The date for which the validation should be checked.
        /// </summary>
        public DateTimeOffset? Date { get; set; }
        /// <summary>
        /// If this parameter has a value of true, the client is stating that the validation is being performed in a context where a concept designated as 'abstract' is appropriate/allowed to be used, and the server should regard abstract codes as valid.
        /// If this parameter is false, abstract codes are not considered to be valid.
        /// </summary>
        public bool? Abstract { get; set; }
        /// <summary>
        /// Specifies the language to be used for description when validating the display property.
        /// </summary>
        public string DisplayLanguage { get; set; }

        public Parameters ToParameters()
        {
            var result = new Parameters();

            if (!string.IsNullOrWhiteSpace(Url))
            {
                result.AddParameterComponent("url", new FhirUri(Url));
            }

            if (!string.IsNullOrWhiteSpace(Context))
            {
                result.AddParameterComponent("context", new FhirUri(Context));
            }

            if (ValueSet != null)
            {
                result.AddParameterComponent("valueSet", ValueSet);
            }

            if (!string.IsNullOrWhiteSpace(ValueSetVersion))
            {
                result.AddParameterComponent("valueSetVersion", new FhirString(ValueSetVersion));
            }

            if (!string.IsNullOrWhiteSpace(Code))
            {
                result.AddParameterComponent("code", new Code(Code));
            }

            if (!string.IsNullOrWhiteSpace(System))
            {
                result.AddParameterComponent("system", new FhirUri(System));
            }

            if (!string.IsNullOrWhiteSpace(SystemVersion))
            {
                result.AddParameterComponent("systemVersion", new FhirString(SystemVersion));
            }

            if (!string.IsNullOrWhiteSpace(Display))
            {
                result.AddParameterComponent("display", new FhirString(Display));
            }

            if (Coding != null)
            {
                result.AddParameterComponent("coding", Coding);
            }

            if (CodeableConcept != null)
            {
                result.AddParameterComponent("codeableConcept", CodeableConcept);
            }

            if (Date.HasValue)
            {
                result.AddParameterComponent("date", new FhirDateTime(Date.Value));
            }

            if (Abstract.HasValue)
            {
                result.AddParameterComponent("abstract", new FhirBoolean(Abstract));
            }

            if (!string.IsNullOrWhiteSpace(DisplayLanguage))
            {
                result.AddParameterComponent("displayLanguage", new Code(DisplayLanguage));
            }

            return result;
        }
    }
}
