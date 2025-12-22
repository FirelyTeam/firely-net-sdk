/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Linq;
using static Hl7.Fhir.Specification.Terminology.ValidateCodeParameters;

namespace Hl7.Fhir.Specification.Terminology;

public static class TerminologyParametersExtensions
{
    extension(Parameters parameters)
    {
        /// <summary>
        /// Validates that there are no duplicate parameters in the provided <see cref="Parameters"/> instance.
        /// </summary>
        /// <exception cref="FhirOperationException"></exception>
        internal Parameters NoDuplicates()
        {
            //No duplicate parameters allowed (http://hl7.org/fhir/valueset-operation-validate-code.html)
            if (parameters.TryGetDuplicates(out var duplicates))
            {
                var message =
                    $"List of input parameters contains the following duplicates: {string.Join(", ", duplicates)}";
                throw FhirOperationException.InvalidOperationInvocation(message);
            }

            return parameters;
        }

        internal ValidateCodeParameters ValidateValueSetValidateCodeParams()
        {
            parameters.NoDuplicates();

            // For input params of https://build.fhir.org/valueset-operation-validate-code.html:
            // * (...) one of the in parameters url, context or valueSet must be provided.
            // * One (and only one) of the in parameters code, coding, or codeableConcept must be provided.
            // * If a code is provided, either a system or inferSystem SHOULD be provided.

            if (!hasValueSet(parameters))
                throw FhirOperationException.InvalidOperationInvocation("'url', 'context' or 'valueset' must be provided.");

            if(!exactlyOneCodeParam(parameters))
                throw FhirOperationException.InvalidOperationInvocation("One (and only one) of 'code', 'coding' or 'codeableConcept' must be provided.");

            if (parameters.HasParam(CODE_ATTRIBUTE) && !exactlyOneSystemParam(parameters))
                throw FhirOperationException.InvalidOperationInvocation("If 'code' is provided, either 'system' or 'inferSystem' must be provided.");

            return new ValidateCodeParameters(parameters);

            static bool hasValueSet(Parameters p) =>
                p.HasParam(URL_ATTRIBUTE) || p.HasParam(CONTEXT_ATTRIBUTE) || p.HasParam(VALUE_SET_ATTRIBUTE);

            static bool exactlyOneCodeParam(Parameters p)
            {
                int count = 0;
                if (p.HasParam(CODE_ATTRIBUTE)) count += 1;
                if (p.HasParam(CODING_ATTRIBUTE)) count += 1;
                if (p.HasParam(CODEABLE_CONCEPT_ATTRIBUTE)) count += 1;
                return count == 1;
            }

            static bool exactlyOneSystemParam(Parameters p)
            {
                int count = 0;
                if (p.HasParam(SYSTEM_ATTRIBUTE)) count += 1;
                if (p.HasParam(INFER_SYSTEM_ATTRIBUTE)) count += 1;
                return count == 1;
            }
        }
    }
}