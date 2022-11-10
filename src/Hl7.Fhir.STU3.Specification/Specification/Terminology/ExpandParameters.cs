/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ExpandParameters
    {
        /// <summary>
        /// A canonical reference to a value set.
        /// </summary>
        public FhirUri Url { get; private set; }
        /// <summary>
        /// The value set is provided directly as part of the request.
        /// </summary>
        public ValueSet ValueSet { get; private set; }
        /// <summary>
        /// The identifier that is used to identify a specific version of the value set to be used when generating the expansion.
        /// </summary>
        public FhirString ValueSetVersion { get; private set; }
        /// <summary>
        /// The context of the value set, so that the server can resolve this to a value set to expand.
        /// </summary>
        public FhirUri Context { get; private set; }
        /// <summary>
        /// If a context is provided, a context direction may also be provided.
        /// </summary>
        public ContextDirection? ContextDirection { get; private set; }
        /// <summary>
        /// A text filter that is applied to restrict the codes that are returned.
        /// </summary>
        public FhirString Filter { get; private set; }
        /// <summary>
        /// The date for which the expansion should be generated.
        /// </summary>
        public FhirDateTime Date { get; private set; }
        /// <summary>
        /// Where to start if a subset is desired (default = 0)
        /// </summary>
        public Integer Offset { get; private set; }
        /// <summary>
        /// How many codes should be provided in a partial page view
        /// </summary>
        public Integer Count { get; private set; }
        /// <summary>
        /// Controls whether concept designations are to be included or excluded in value set expansions.
        /// </summary>
        public FhirBoolean IncludeDesignations { get; private set; }
        /// <summary>
        /// A token that specifies a system+code that is either a use or a language.
        /// </summary>
        public IEnumerable<FhirString> Designation { get; private set; }
        /// <summary>
        /// Controls whether the value set definition is included or excluded in value set expansions.
        /// </summary>
        public FhirBoolean IncludeDefinition { get; private set; }
        /// <summary>
        /// Controls whether inactive concepts are included or excluded in value set expansions.
        /// </summary>
        public FhirBoolean ActiveOnly { get; private set; }
        /// <summary>
        /// Controls whether or not the value set expansion nests codes or not (i.e. ValueSet.expansion.contains.contains).
        /// </summary>
        public FhirBoolean ExcludeNested { get; private set; }
        /// <summary>
        /// Controls whether or not the value set expansion is assembled for a user interface use or not.
        /// </summary>
        public FhirBoolean ExcludeNotForUI { get; private set; }
        /// <summary>
        /// Controls whether or not the value set expansion includes post coordinated codes.
        /// </summary>
        public FhirBoolean ExcludePostCoordinated { get; private set; }
        /// <summary>
        /// Specifies the language to be used for description in the expansions i.e. the language to be used for ValueSet.expansion.contains.display
        /// </summary>
        public Code DisplayLanguage { get; private set; }
        /// <summary>
        /// Code system, or a particular version of a code system to be excluded from the value set expansion.
        /// </summary>
        /// <remarks> The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
        public IEnumerable<Canonical> ExcludeSystem { get; private set; }
        /// <summary>
        /// Specifies a version to use for a system, if the value set does not specify which one to use.
        /// </summary>
        /// <remarks>The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
        public IEnumerable<Canonical> SystemVersion { get; private set; }
        /// <summary>
        /// Specifies a version to use for a system. If a value set specifies a different version, an error is returned instead of the expansion.
        /// </summary>
        /// <remarks>The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
        public IEnumerable<Canonical> CheckSystemVersion { get; private set; }
        /// <summary>
        /// Specifies a version to use for a system. This parameter overrides any specified version in the value set (and any it depends on).
        /// </summary>
        /// <remarks>The format is the same as a canonical URL: [system]|[version] - e.g. http://loinc.org|2.56.</remarks>
        public IEnumerable<Canonical> ForceSystemVersion { get; private set; }

        #region Build methods
        public ExpandParameters WithValueSet(string url = null, ValueSet valueSet = null, string valueSetVersion = null, string context = null, ContextDirection? contextDirection = null)
        {
            if (!string.IsNullOrWhiteSpace(url)) Url = new FhirUri(url);
            ValueSet = valueSet;
            if (!string.IsNullOrWhiteSpace(valueSetVersion)) ValueSetVersion = new FhirString(valueSetVersion);
            if (!string.IsNullOrWhiteSpace(context)) Context = new FhirUri(context);
            ContextDirection = contextDirection;
            return this;
        }
        public ExpandParameters WithFilter(string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter)) Filter = new FhirString(filter);
            return this;
        }

        public ExpandParameters WithPaging(int? offset = null, int? count = null)
        {
            if (offset.HasValue) Offset = new Integer(offset);
            if (count.HasValue) Count = new Integer(count);
            return this;
        }

        public ExpandParameters WithDesignation(bool? includeDesignation = null, string[] designations = null)
        {
            if (includeDesignation.HasValue) IncludeDesignations = new FhirBoolean(includeDesignation);
            Designation = designations?.Select(d => new FhirString(d));
            return this;
        }
        #endregion

        public Parameters Build()
        {
            var result = new Parameters();

            if (Url is { }) result.Add("url", Url);
            if (ValueSet != null) result.Add("valueSet", ValueSet);
            if (ValueSetVersion is { }) result.Add("valueSetVersion", ValueSetVersion);
            if (Context is { }) result.Add("context", Context);
            if (ContextDirection.HasValue) result.Add("contextDirection", new Code(ContextDirection.GetLiteral()));
            if (Filter is { }) result.Add("filter", Filter);
            if (Date is { }) result.Add("date", Date);
            if (Offset is { }) result.Add("offset", Offset);
            if (Count is { }) result.Add("count", Count);
            if (IncludeDesignations is { }) result.Add("includeDesignations", IncludeDesignations);

            foreach (var designation in Designation ?? Enumerable.Empty<FhirString>())
            {
                result.Add("designation", designation);
            }

            if (IncludeDefinition is { }) result.Add("includeDefinition", IncludeDefinition);
            if (ActiveOnly is { }) result.Add("activeOnly", ActiveOnly);
            if (ExcludeNested is { }) result.Add("excludeNested", ExcludeNested);
            if (ExcludeNotForUI is { }) result.Add("excludeNotForUI", ExcludeNotForUI);
            if (ExcludePostCoordinated is { }) result.Add("excludePostCoordinated", ExcludePostCoordinated);
            if (DisplayLanguage is { }) result.Add("displayLanguage", DisplayLanguage);

            foreach (var excludeSystem in ExcludeSystem ?? Enumerable.Empty<Canonical>())
            {
                result.Add("excludeSystem", excludeSystem);
            }
            foreach (var systemVersion in SystemVersion ?? Enumerable.Empty<Canonical>())
            {
                result.Add("systemVersion", systemVersion);
            }
            foreach (var checkSystemVersion in CheckSystemVersion ?? Enumerable.Empty<Canonical>())
            {
                result.Add("checkSystemVersion", checkSystemVersion);
            }

            foreach (var forceSystemVersion in ForceSystemVersion ?? Enumerable.Empty<Canonical>())
            {
                result.Add("forceSystemVersion", forceSystemVersion);
            }
            return result;
        }
    }
}
