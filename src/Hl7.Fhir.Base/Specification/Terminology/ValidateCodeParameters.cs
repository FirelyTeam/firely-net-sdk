/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ValidateCodeParameters
    {
        private const string URL_ATTRIBUTE = "url";
        private const string CONTEXT_ATTRIBUTE = "context";
        private const string VALUE_SET_ATTRIBUTE = "valueSet";
        private const string VALUE_SET_VERSION_ATTRIBUTE = "valueSetVersion";
        private const string CODE_ATTRIBUTE = "code";
        private const string SYSTEM_ATTRIBUTE = "system";
        private const string SYSTEM_VERSION_ATTRIBUTE = "systemVersion";
        private const string DISPLAY_ATTRIBUTE = "display";
        private const string CODING_ATTRIBUTE = "coding";
        private const string CODEABLE_CONCEPT_ATTRIBUTE = "codeableConcept";
        private const string DATE_ATTRIBUTE = "date";
        private const string ABSTRACT_ATTRIBUTE = "abstract";
        private const string DISPLAY_LANGUAGE_ATTRIBUTE = "displayLanguage";
        private const string INFER_SYSTEM_ATTRIBUTE = "inferSystem";

        public ValidateCodeParameters(Parameters parameters)
        {
            Url = parameters.GetSingleValue<FhirUri>(URL_ATTRIBUTE);
            Context = parameters.GetSingleValue<FhirUri>(CONTEXT_ATTRIBUTE);
            ValueSet = parameters.GetSingle(VALUE_SET_ATTRIBUTE)?.Resource;
            ValueSetVersion = parameters.GetSingleValue<FhirString>(VALUE_SET_VERSION_ATTRIBUTE);
            Code = parameters.GetSingleValue<Code>(CODE_ATTRIBUTE);
            System = parameters.GetSingleValue<FhirUri>(SYSTEM_ATTRIBUTE);
            SystemVersion = parameters.GetSingleValue<FhirString>(SYSTEM_VERSION_ATTRIBUTE);
            Display = parameters.GetSingleValue<FhirString>(DISPLAY_ATTRIBUTE);
            Coding = parameters.GetSingleValue<Coding>(CODING_ATTRIBUTE);
            CodeableConcept = parameters.GetSingleValue<CodeableConcept>(CODEABLE_CONCEPT_ATTRIBUTE);
            Date = parameters.GetSingleValue<FhirDateTime>(DATE_ATTRIBUTE);
            Abstract = parameters.GetSingleValue<FhirBoolean>(ABSTRACT_ATTRIBUTE);
            DisplayLanguage = parameters.GetSingleValue<Code>(DISPLAY_LANGUAGE_ATTRIBUTE);
            InferSystem = parameters.GetSingleValue<FhirBoolean>(INFER_SYSTEM_ATTRIBUTE);
        }


        public ValidateCodeParameters()
        {
        }

        #region Builder methods
        public ValidateCodeParameters WithValueSet(string? url, string? context = null, Resource? valueSet = null, string? valueSetVersion = null)
        {
            if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
            if (!string.IsNullOrWhiteSpace(context)) Context = new FhirUri(context);
            ValueSet = valueSet;
            if (!string.IsNullOrWhiteSpace(valueSetVersion)) ValueSetVersion = new FhirString(valueSetVersion);
            return this;
        }

        /// <summary>
        /// Takes a canonical and splits it into the correct "url", and "valueSetVersion" parameters. 
        /// </summary>
        /// <param name="canonical">Canonical to be split up</param>
        /// <returns></returns>
        public ValidateCodeParameters WithValueSet(Canonical canonical)
        {
            var (uri, version, fragment) = canonical;
            Url = new FhirUri(new Canonical(uri, null, fragment));
            if (!string.IsNullOrWhiteSpace(version)) ValueSetVersion = new FhirString(version);
            return this;
        }

        public ValidateCodeParameters WithCode(string? code = null, string? system = null,
            string? systemVersion = null, string? display = null, string? displayLanguage = null,
            string? context = null, bool? inferSystem = null)
        {
            if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
            if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
            if (!string.IsNullOrWhiteSpace(systemVersion)) SystemVersion = new FhirString(systemVersion);
            if (!string.IsNullOrWhiteSpace(display)) Display = new FhirString(display);
            if (!string.IsNullOrWhiteSpace(displayLanguage)) DisplayLanguage = new Code(displayLanguage);
            if (!string.IsNullOrWhiteSpace(context)) Context = new FhirUri(context);
            if (inferSystem is not null) InferSystem = new FhirBoolean(inferSystem);
            return this;
        }

        public ValidateCodeParameters WithCoding(Coding? coding)
        {
            Coding = coding;
            return this;
        }

        public ValidateCodeParameters WithCodeableConcept(CodeableConcept? codeableConcept)
        {
            CodeableConcept = codeableConcept;
            return this;
        }

        public ValidateCodeParameters WithDate(FhirDateTime? date)
        {
            Date = date;
            return this;
        }

        public ValidateCodeParameters WithAbstract(bool? @abstract)
        {
            Abstract = @abstract.HasValue ? new FhirBoolean(@abstract) : null;
            return this;
        }
        #endregion

        /// <summary>
        /// A canonical reference to a value set.
        /// </summary>
        public FhirUri? Url { get; private set; }
        /// <summary>
        /// The context of the value set, so that the server can resolve this to a value set to validate against.
        /// </summary>
        public FhirUri? Context { get; private set; }
        /// <summary>
        /// The value set is provided directly as part of the request.
        /// </summary>
        public Resource? ValueSet { get; private set; }
        /// <summary>
        /// The identifier that is used to identify a specific version of the value set to be used when validating the code.
        /// </summary>
        public FhirString? ValueSetVersion { get; private set; }
        /// <summary>
        /// The code that is to be validated.
        /// </summary>
        /// <remarks>If a code is provided, a system or a context must be provided.</remarks>
        public Code? Code { get; private set; }
        /// <summary>
        /// The system for the code that is to be validated
        /// </summary>
        public FhirUri? System { get; private set; }
        /// <summary>
        /// The version of the system.
        /// </summary>
        public FhirString? SystemVersion { get; private set; }
        /// <summary>
        /// The display associated with the code.
        /// </summary>
        /// <remarks>If a display is provided a code must be provided.</remarks>
        public FhirString? Display { get; private set; }
        /// <summary>
        /// A coding to validate.
        /// </summary>
        public Coding? Coding { get; private set; }
        /// <summary>
        /// A full codeableConcept to validate.
        /// </summary>
        /// <remarks>The server returns true if one of the coding values is in the value set, and may also validate that the codings are not in conflict with each other if more than one is present.</remarks>
        public CodeableConcept? CodeableConcept { get; private set; }
        /// <summary>
        /// The date for which the validation should be checked.
        /// </summary>
        public FhirDateTime? Date { get; private set; }
        /// <summary>
        /// If this parameter has a value of true, the client is stating that the validation is being performed in a context where a concept designated as 'abstract' is appropriate/allowed to be used, and the server should regard abstract codes as valid.
        /// If this parameter is false, abstract codes are not considered to be valid.
        /// </summary>
        public FhirBoolean? Abstract { get; private set; }
        /// <summary>
        /// Specifies the language to be used for description when validating the display property.
        /// </summary>
        public Code? DisplayLanguage { get; private set; }

        public FhirBoolean? InferSystem { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Parameters Build()
        {
            var result = new Parameters();

            if (Url is not null) result.Add(URL_ATTRIBUTE, Url);
            if (ValueSet is not null) result.Add(VALUE_SET_ATTRIBUTE, ValueSet);
            if (Context is not null) result.Add(CONTEXT_ATTRIBUTE, Context);
            if (ValueSetVersion is not null) result.Add(VALUE_SET_VERSION_ATTRIBUTE, ValueSetVersion);
            if (Code is not null) result.Add(CODE_ATTRIBUTE, Code);
            if (System is not null) result.Add(SYSTEM_ATTRIBUTE, System);
            if (SystemVersion is not null) result.Add(SYSTEM_VERSION_ATTRIBUTE, SystemVersion);
            if (Display is not null) result.Add(DISPLAY_ATTRIBUTE, Display);
            if (Coding is not null) result.Add(CODING_ATTRIBUTE, Coding);
            if (CodeableConcept is not null) result.Add(CODEABLE_CONCEPT_ATTRIBUTE, CodeableConcept);
            if (Date is not null) result.Add(DATE_ATTRIBUTE, Date);
            if (Abstract is not null) result.Add(ABSTRACT_ATTRIBUTE, Abstract);
            if (DisplayLanguage is not null) result.Add(DISPLAY_LANGUAGE_ATTRIBUTE, DisplayLanguage);
            if (InferSystem is not null) result.Add(INFER_SYSTEM_ATTRIBUTE, InferSystem);
            return result;
        }
    }
}