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
    public class TranslateParameters
    {
        /// <summary>
        /// A canonical URL for a concept map.
        /// </summary>
        public FhirUri Url { get; private set; }
        /// <summary>
        /// The concept map is provided directly as part of the request.
        /// </summary>
        public ConceptMap ConceptMap { get; private set; }
        /// <summary>
        /// The identifier that is used to identify a specific version of the concept map to be used for the translation.
        /// </summary>
        public FhirString ConceptMapVersion { get; private set; }
        /// <summary>
        /// The code that is to be translated. If a code is provided, a system must be provided.
        /// </summary>
        public Code Code { get; private set; }
        /// <summary>
        /// The system for the code that is to be translated
        /// </summary>
        public FhirUri System { get; private set; }
        /// <summary>
        /// The version of the system, if one was provided in the source data.
        /// </summary>
        public FhirString Version { get; private set; }
        /// <summary>
        /// Identifies the value set used when the concept (system/code pair) was chosen. May be a logical id, or an absolute or relative location.
        /// </summary>
        public FhirUri Source { get; private set; }
        /// <summary>
        /// A coding to translate
        /// </summary>
        public Coding Coding { get; private set; }
        /// <summary>
        /// A full codeableConcept to validate.
        /// </summary>
        public CodeableConcept CodeableConcept { get; private set; }
        /// <summary>
        /// Identifies the value set in which a translation is sought.
        /// </summary>
        public FhirUri Target { get; private set; }
        /// <summary>
        /// identifies a target code system in which a mapping is sought. This parameter is an alternative to the target parameter - only one is required.
        /// </summary>
        public FhirUri TargetSystem { get; private set; }
        ///// <summary>
        ///// Another element that may help produce the correct mapping
        ///// </summary>
        //public List<OfSomething> Dependency { get; set; }
        /// <summary>
        /// If this is true, then the operation should return all the codes that might be mapped to this code. This parameter reverses the meaning of the source and target parameters
        /// </summary>
        public FhirBoolean Reverse { get; private set; }

        #region Builder methods
        public TranslateParameters WithConceptMap(string url = null, ConceptMap conceptMap = null, string conceptMapVersion = null, string source = null)
        {
            if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
            ConceptMap = conceptMap;
            if (!string.IsNullOrWhiteSpace(conceptMapVersion)) ConceptMapVersion = new FhirString(conceptMapVersion);
            if (!string.IsNullOrWhiteSpace(source)) Source = new FhirUri(source);
            return this;
        }

        public TranslateParameters WithCode(string code, string system = null, string version = null)
        {
            if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
            if (!string.IsNullOrWhiteSpace(code)) System = new FhirUri(system);
            if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);
            return this;
        }
        public TranslateParameters WithCoding(Coding coding)
        {
            Coding = coding;
            return this;
        }

        public TranslateParameters WithCodeableConcept(CodeableConcept codeableConcept)
        {
            CodeableConcept = codeableConcept;
            return this;
        }

        public TranslateParameters WithTarget(string target, string targetSystem = null)
        {
            if (!string.IsNullOrWhiteSpace(target)) Target = new FhirUri(target);
            if (!string.IsNullOrWhiteSpace(targetSystem)) TargetSystem = new FhirUri(targetSystem);
            return this;
        }

        public TranslateParameters WithReverse(bool? reverse)
        {
            if (reverse.HasValue) Reverse = new FhirBoolean(reverse);
            return this;
        }
        #endregion

        public Parameters Build()
        {
            var result = new Parameters();
            if (Url is { }) result.Add("url", Url);
            if (ConceptMap is { }) result.Add("conceptMap", ConceptMap);
            if (ConceptMapVersion is { }) result.Add("conceptMapVersion", ConceptMapVersion);
            if (Code is { }) result.Add("code", Code);
            if (System is { }) result.Add("system", System);
            if (Version is { }) result.Add("version", Version);
            if (Source is { }) result.Add("source", Source);
            if (Coding is { }) result.Add("coding", Coding);
            if (CodeableConcept is { }) result.Add("codeableConcept", CodeableConcept);
            if (Target is { }) result.Add("target", Target);
            if (TargetSystem is { }) result.Add("targetSystem", TargetSystem);
            if (Reverse is { }) result.Add("reverse", Reverse);
            return result;
        }
    }
}
