/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ValidateCodeParameters
    {
        private readonly string _urlAttribute = "url";
        private readonly string _contextAttribute = "context";
        private readonly string _valueSetAttribute = "valueSet";
        private readonly string _valueSetVersionAttribute = "valueSetVersion";
        private readonly string _codeAttribute = "code";
        private readonly string _systemAttribute = "system";
        private readonly string _systemVersionAttribute = "systemVersion";
        private readonly string _displayAttribute = "display";
        private readonly string _codingAttribute = "coding";
        private readonly string _codeableConceptAttribute = "codeableConcept";
        private readonly string _dateAttribute = "date";
        private readonly string _abstractAttribute = "abstract";
        private readonly string _displayLanguageAttribute = "displayLanguage";

        public ValidateCodeParameters(Parameters parameters)
        {
            Url = parameters.GetSingleValue<FhirUri>(_urlAttribute);
            Context = parameters.GetSingleValue<FhirUri>(_contextAttribute);
            ValueSet = parameters.GetSingle(_valueSetAttribute)?.Resource;
            ValueSetVersion = parameters.GetSingleValue<FhirString>(_valueSetVersionAttribute);
            Code = parameters.GetSingleValue<Code>(_codeAttribute);
            System = parameters.GetSingleValue<FhirUri>(_systemAttribute);
            SystemVersion = parameters.GetSingleValue<FhirString>(_systemVersionAttribute);
            Display = parameters.GetSingleValue<FhirString>(_displayAttribute);
            Coding = parameters.GetSingleValue<Coding>(_codingAttribute);
            CodeableConcept = parameters.GetSingleValue<CodeableConcept>(_codeableConceptAttribute);
            Date = parameters.GetSingleValue<FhirDateTime>(_dateAttribute);
            Abstract = parameters.GetSingleValue<FhirBoolean>(_abstractAttribute);
            DisplayLanguage = parameters.GetSingleValue<Code>(_displayLanguageAttribute);
        }


        public ValidateCodeParameters()
        {
        }

        #region Builder methods
        public ValidateCodeParameters WithValueSet(string url, string context = null, Resource valueSet = null, string valueSetVersion = null)
        {
            if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
            if (!string.IsNullOrWhiteSpace(context)) Context = new FhirUri(context);
            ValueSet = valueSet;
            if (!string.IsNullOrWhiteSpace(valueSetVersion)) ValueSetVersion = new FhirString(valueSetVersion);
            return this;
        }

        public ValidateCodeParameters WithCode(string code = null, string system = null, string systemVersion = null, string display = null, string displayLanguage = null, string context = null)
        {
            if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
            if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
            if (!string.IsNullOrWhiteSpace(systemVersion)) SystemVersion = new FhirString(systemVersion);
            if (!string.IsNullOrWhiteSpace(display)) Display = new FhirString(display);
            if (!string.IsNullOrWhiteSpace(displayLanguage)) DisplayLanguage = new Code(displayLanguage);
            if (!string.IsNullOrWhiteSpace(context)) Context = new FhirUri(context);
            return this;
        }

        public ValidateCodeParameters WithCoding(Coding coding)
        {
            Coding = coding;
            return this;
        }

        public ValidateCodeParameters WithCodeableConcept(CodeableConcept codeableConcept)
        {
            CodeableConcept = codeableConcept;
            return this;
        }

        public ValidateCodeParameters WithDate(FhirDateTime date)
        {
            Date = date;
            return this;
        }

        public ValidateCodeParameters WithAbstract(bool? @abstract)
        {
            if (@abstract.HasValue) Abstract = new FhirBoolean(@abstract);
            return this;
        }
        #endregion

        /// <summary>
        /// A canonical reference to a value set.
        /// </summary>
        public FhirUri Url { get; private set; }
        /// <summary>
        /// The context of the value set, so that the server can resolve this to a value set to validate against.
        /// </summary>
        public FhirUri Context { get; private set; }
        /// <summary>
        /// The value set is provided directly as part of the request.
        /// </summary>
        public Resource ValueSet { get; private set; }
        /// <summary>
        /// The identifier that is used to identify a specific version of the value set to be used when validating the code.
        /// </summary>
        public FhirString ValueSetVersion { get; private set; }
        /// <summary>
        /// The code that is to be validated.
        /// </summary>
        /// <remarks>If a code is provided, a system or a context must be provided.</remarks>
        public Code Code { get; private set; }
        /// <summary>
        /// The system for the code that is to be validated
        /// </summary>
        public FhirUri System { get; private set; }
        /// <summary>
        /// The version of the system.
        /// </summary>
        public FhirString SystemVersion { get; private set; }
        /// <summary>
        /// The display associated with the code.
        /// </summary>
        /// <remarks>If a display is provided a code must be provided.</remarks>
        public FhirString Display { get; private set; }
        /// <summary>
        /// A coding to validate.
        /// </summary>
        public Coding Coding { get; private set; }
        /// <summary>
        /// A full codeableConcept to validate.
        /// </summary>
        /// <remarks>The server returns true if one of the coding values is in the value set, and may also validate that the codings are not in conflict with each other if more than one is present.</remarks>
        public CodeableConcept CodeableConcept { get; private set; }
        /// <summary>
        /// The date for which the validation should be checked.
        /// </summary>
        public FhirDateTime Date { get; private set; }
        /// <summary>
        /// If this parameter has a value of true, the client is stating that the validation is being performed in a context where a concept designated as 'abstract' is appropriate/allowed to be used, and the server should regard abstract codes as valid.
        /// If this parameter is false, abstract codes are not considered to be valid.
        /// </summary>
        public FhirBoolean Abstract { get; private set; }
        /// <summary>
        /// Specifies the language to be used for description when validating the display property.
        /// </summary>
        public Code DisplayLanguage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Parameters Build()
        {
            var result = new Parameters();

            if (Url is { }) result.Add(_urlAttribute, Url);
            if (Context is { }) result.Add(_contextAttribute, Context);
            if (ValueSet is { }) result.Add(_valueSetAttribute, ValueSet);
            if (ValueSetVersion is { }) result.Add(_valueSetVersionAttribute, ValueSetVersion);
            if (Code is { }) result.Add(_codeAttribute, Code);
            if (System is { }) result.Add(_systemAttribute, System);
            if (SystemVersion is { }) result.Add(_systemVersionAttribute, SystemVersion);
            if (Display is { }) result.Add(_displayAttribute, Display);
            if (Coding is { }) result.Add(_codingAttribute, Coding);
            if (CodeableConcept is { }) result.Add(_codeableConceptAttribute, CodeableConcept);
            if (Date is { }) result.Add(_dateAttribute, Date);
            if (Abstract is { }) result.Add(_abstractAttribute, Abstract);
            if (DisplayLanguage is { }) result.Add(_displayAttribute, DisplayLanguage);

            return result;
        }
    }
}