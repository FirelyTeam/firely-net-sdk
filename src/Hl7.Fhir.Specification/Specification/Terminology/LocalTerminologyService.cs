/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LocalTerminologyService : ITerminologyService
    {
        //private IConformanceSource _source;
        private IResourceResolver _resolver;
        private ValueSetExpander _expander;

        public LocalTerminologyService(IResourceResolver resolver, ValueSetExpanderSettings expanderSettings = null)
        {
            _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

            var settings = expanderSettings ?? ValueSetExpanderSettings.Default;
            if (settings.ValueSetSource == null) settings.ValueSetSource = resolver;

            _expander = new ValueSetExpander(settings);
        }

        internal ValueSet FindValueset(string canonical)
        {
            return _resolver.FindValueSet(canonical);
        }

        public OperationOutcome ValidateCode(string uri, string code, string system, string display = null, bool abstractAllowed = false)
        {
            if (string.IsNullOrEmpty(uri)) throw Error.ArgumentNullOrEmpty(nameof(uri));
            if (string.IsNullOrEmpty(code)) throw Error.ArgumentNullOrEmpty(nameof(code));

            var result = new OperationOutcome();

            ValueSet vs = null;
            try
            {
                vs = _resolver.FindValueSet(uri);
            }
            catch
            {
                vs = null;
            }

            if (vs == null)
                throw new ValueSetUnknownException($"Cannot retrieve valueset '{uri}'");

            return ValidateCode(vs, code, system, display, abstractAllowed);
        }


        public OperationOutcome ValidateCode(ValueSet vs, string code, string system, string display = null, bool abstractAllowed = false)
        {
            if (vs == null) throw Error.ArgumentNull(nameof(vs));
            if (string.IsNullOrEmpty(code)) throw Error.ArgumentNullOrEmpty(nameof(code));

            // We might have a cached or pre-expanded version brought to us by the _source
            if (!vs.HasExpansion)
            {
                // This will expand te vs - since we do not deepcopy() it, it will change the instance
                // as it was passed to us from the source
                _expander.Expand(vs);
            }

            var component = vs.FindInExpansion(code, system);
            var codeLabel = $"Code '{code}' from system '{system}'";
            var result = new OperationOutcome();

            if (component == null)
                result.AddIssue($"{codeLabel} does not exist in valueset '{vs.Url}'", Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
            else
            {
                if (component.Abstract == true && !abstractAllowed)
                    result.AddIssue($"{codeLabel} is abstract, which is not allowed here", Issue.TERMINOLOGY_ABSTRACT_CODE_NOT_ALLOWED);

                if (display != null && component.Display != null && display != component.Display)
                    result.AddIssue($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'", Issue.TERMINOLOGY_INCORRECT_DISPLAY);
            }

            return result;
        }
    }
}

