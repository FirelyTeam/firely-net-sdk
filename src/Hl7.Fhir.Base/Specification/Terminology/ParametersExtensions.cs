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

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Extension methods for Parameters used in terminology operations.
/// </summary>
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
    }
}