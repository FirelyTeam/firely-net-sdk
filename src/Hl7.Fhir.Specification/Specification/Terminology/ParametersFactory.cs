/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public static class ParametersFactory
    {
        public static Parameters CreateExpandParameters(ExpandParameters parameters)
        {
            var @params = new Parameters();

            if(!string.IsNullOrWhiteSpace(parameters.Url))
            {
                @params.AddParameterComponent("url", new FhirUri(parameters.Url));
            }

            if (parameters.ValueSet != null)
            {
                @params.AddParameterComponent("valueSet", parameters.ValueSet);
            }

            if (!string.IsNullOrWhiteSpace(parameters.ValueSetVersion))
            {
                @params.AddParameterComponent("valueSetVersion", new FhirString(parameters.ValueSetVersion));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Context))
            {
                @params.AddParameterComponent("context", new FhirUri(parameters.Context));
            }

            if (parameters.ContextDirection.HasValue)
            {
                @params.AddParameterComponent("contextDirection", new Code(parameters.ContextDirection.GetLiteral()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                @params.AddParameterComponent("filter", new FhirString(parameters.Filter));
            }

            if (parameters.Date.HasValue)
            {
                @params.AddParameterComponent("date", new FhirDateTime(parameters.Date.Value));
            }

            if (parameters.Offset.HasValue)
            {
                @params.AddParameterComponent("offset", new Integer(parameters.Offset));
            }

            if (parameters.Count.HasValue)
            {
                @params.AddParameterComponent("count", new Integer(parameters.Count));
            }

            if (parameters.IncludeDesignations.HasValue)
            {
                @params.AddParameterComponent("includeDesignations", new FhirBoolean(parameters.IncludeDesignations));
            }

            if (parameters.Designation?.Count > 0)
            {
                foreach (var designation in parameters.Designation)
                {
                    if (string.IsNullOrWhiteSpace(designation)) continue;
                    @params.AddParameterComponent("designation", new FhirString(designation));
                }
            }

            if (parameters.IncludeDefinition.HasValue)
            {
                @params.AddParameterComponent("includeDefinition", new FhirBoolean(parameters.IncludeDefinition));
            }

            if (parameters.ActiveOnly.HasValue)
            {
                @params.AddParameterComponent("activeOnly", new FhirBoolean(parameters.ActiveOnly));
            }

            if (parameters.ExcludeNested.HasValue)
            {
                @params.AddParameterComponent("excludeNested", new FhirBoolean(parameters.ExcludeNested));
            }

            if (parameters.ExcludeNotForUI.HasValue)
            {
                @params.AddParameterComponent("excludeNotForUI", new FhirBoolean(parameters.ExcludeNotForUI));
            }

            if (parameters.ExcludePostCoordinated.HasValue)
            {
                @params.AddParameterComponent("excludePostCoordinated", new FhirBoolean(parameters.ExcludePostCoordinated));
            }

            if (!string.IsNullOrWhiteSpace(parameters.DisplayLanguage))
            {
                @params.AddParameterComponent("displayLanguage", new Code(parameters.DisplayLanguage));
            }

            if (parameters.ExcludeSystem?.Count > 0)
            {
                foreach (var excludeSystem in parameters.ExcludeSystem)
                {
                    if (string.IsNullOrWhiteSpace(excludeSystem)) continue;
                    @params.AddParameterComponent("excludeSystem", new Canonical(excludeSystem));
                }
            }

            if (parameters.SystemVersion?.Count > 0)
            {
                foreach (var systemVersion in parameters.SystemVersion)
                {
                    if (string.IsNullOrWhiteSpace(systemVersion)) continue;
                    @params.AddParameterComponent("systemVersion", new Canonical(systemVersion));
                }
            }

            if (parameters.CheckSystemVersion?.Count > 0)
            {
                foreach (var checkSystemVersion in parameters.CheckSystemVersion)
                {
                    if (string.IsNullOrWhiteSpace(checkSystemVersion)) continue;
                    @params.AddParameterComponent("checkSystemVersion", new Canonical(checkSystemVersion));
                }
            }

            if (parameters.ForceSystemVersion?.Count > 0)
            {
                foreach (var forceSystemVersion in parameters.ForceSystemVersion)
                {
                    if (string.IsNullOrWhiteSpace(forceSystemVersion)) continue;
                    @params.AddParameterComponent("forceSystemVersion", new Canonical(forceSystemVersion));
                }
            }

            return @params;
        }

        public static Parameters CreateValidateCodeParameters(ValidateCodeParameters parameters)
        {
            var @params = new Parameters();

            if (!string.IsNullOrWhiteSpace(parameters.Url))
            {
                @params.AddParameterComponent("url", new FhirUri(parameters.Url));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Context))
            {
                @params.AddParameterComponent("context", new FhirUri(parameters.Context));
            }

            if (parameters.ValueSet != null)
            {
                @params.AddParameterComponent("valueSet", parameters.ValueSet);
            }

            if (!string.IsNullOrWhiteSpace(parameters.ValueSetVersion))
            {
                @params.AddParameterComponent("valueSetVersion", new FhirString(parameters.ValueSetVersion));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Code))
            {
                @params.AddParameterComponent("code", new Code(parameters.Code));
            }

            if (!string.IsNullOrWhiteSpace(parameters.System))
            {
                @params.AddParameterComponent("system", new FhirUri(parameters.System));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SystemVersion))
            {
                @params.AddParameterComponent("systemVersion", new FhirString(parameters.SystemVersion));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Display))
            {
                @params.AddParameterComponent("display", new FhirString(parameters.Display));
            }

            if (parameters.Coding != null)
            {
                @params.AddParameterComponent("coding", parameters.Coding);
            }

            if (parameters.CodeableConcept != null)
            {
                @params.AddParameterComponent("codeableConcept", parameters.CodeableConcept);
            }

            if (parameters.Date.HasValue)
            {
                @params.AddParameterComponent("date", new FhirDateTime(parameters.Date.Value));
            }

            if (parameters.Abstract.HasValue)
            {
                @params.AddParameterComponent("abstract", new FhirBoolean(parameters.Abstract));
            }

            if (!string.IsNullOrWhiteSpace(parameters.DisplayLanguage))
            {
                @params.AddParameterComponent("displayLanguage", new Code(parameters.DisplayLanguage));
            }

            return @params;
        }

        public static Parameters CreateLookupParameters(LookupParameters parameters)
        {
            var @params = new Parameters();

            if(!string.IsNullOrWhiteSpace(parameters.Code))
            {
                @params.AddParameterComponent("code", new Code(parameters.Code));
            }

            if (!string.IsNullOrWhiteSpace(parameters.System))
            {
                @params.AddParameterComponent("system", new FhirUri(parameters.System));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Version))
            {
                @params.AddParameterComponent("version", new FhirString(parameters.Version));
            }

            if (parameters.Coding != null)
            {
                @params.AddParameterComponent("coding", parameters.Coding);
            }

            if (parameters.Date.HasValue)
            {
                @params.AddParameterComponent("date", new FhirDateTime(parameters.Date.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.DisplayLanguage))
            {
                @params.AddParameterComponent("displayLanguage", new Code(parameters.DisplayLanguage));
            }

            if(parameters.Property?.Count > 0)
            {
                foreach(var prop in parameters.Property)
                {
                    if (string.IsNullOrWhiteSpace(prop)) continue;
                    @params.AddParameterComponent("property", new Code(prop));
                }
            }

            return @params;
        }

        public static Parameters CreateSubsumesParameters(SubsumesParameters parameters)
        {
            var @params = new Parameters();

            if (!string.IsNullOrWhiteSpace(parameters.CodeA))
            {
                @params.AddParameterComponent("codeA", new Code(parameters.CodeA));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodeB))
            {
                @params.AddParameterComponent("codeB", new Code(parameters.CodeB));
            }

            if (!string.IsNullOrWhiteSpace(parameters.System))
            {
                @params.AddParameterComponent("system", new FhirUri(parameters.System));
            }

            if (parameters.CodingA != null)
            {
                @params.AddParameterComponent("codingA", parameters.CodingA);
            }

            if (parameters.CodingB != null)
            {
                @params.AddParameterComponent("codingB", parameters.CodingB);
            }

            return @params;
        }

        public static Parameters CreateTranslateParameters(TranslateParameters parameters)
        {
            var @params = new Parameters();

            if (!string.IsNullOrWhiteSpace(parameters.Url))
            {
                @params.AddParameterComponent("url", new FhirUri(parameters.Url));
            }

            if (parameters.ConceptMap != null)
            {
                @params.AddParameterComponent("conceptMap", parameters.ConceptMap);
            }

            if (!string.IsNullOrWhiteSpace(parameters.ConceptMapVersion))
            {
                @params.AddParameterComponent("conceptMapVersion", new FhirString(parameters.ConceptMapVersion));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Code))
            {
                @params.AddParameterComponent("code", new Code(parameters.Code));
            }

            if (!string.IsNullOrWhiteSpace(parameters.System))
            {
                @params.AddParameterComponent("system", new FhirUri(parameters.System));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Version))
            {
                @params.AddParameterComponent("version", new FhirString(parameters.Version));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Source))
            {
                @params.AddParameterComponent("source", new FhirUri(parameters.Source));
            }

            if (parameters.Coding != null)
            {
                @params.AddParameterComponent("coding", parameters.Coding);
            }

            if (parameters.CodeableConcept != null)
            {
                @params.AddParameterComponent("codeableConcept", parameters.CodeableConcept);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Target))
            {
                @params.AddParameterComponent("target", new FhirUri(parameters.Target));
            }

            if (!string.IsNullOrWhiteSpace(parameters.TargetSystem))
            {
                @params.AddParameterComponent("targetSystem", new FhirUri(parameters.TargetSystem));
            }

            if(parameters.Reverse.HasValue)
            {
                @params.AddParameterComponent("reverse", new FhirBoolean(parameters.Reverse.Value));
            }

            return @params;
        }

        public static Parameters CreateClosureParameters(ClosureParameters parameters)
        {
            var @params = new Parameters();

            if(!string.IsNullOrWhiteSpace(parameters.Name))
            {
                @params.AddParameterComponent("name", new FhirString(parameters.Name));
            }

            if (parameters.Concept != null)
            {
                @params.AddParameterComponent("concept", parameters.Concept);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Version))
            {
                @params.AddParameterComponent("version", new FhirString(parameters.Version));
            }

            return @params;
        }
    }
}
