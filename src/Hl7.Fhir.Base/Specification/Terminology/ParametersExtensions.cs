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
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Extension methods for Parameters used in terminology operations.
/// </summary>
public static class TerminologyParametersExtensions
{
    extension(Parameters parameters)
    {
        /// <summary>
        /// Validates that none of the <paramref name="singleValued"/> parameters (the operation's 0..1 parameters)
        /// occur more than once. Other parameters, such as the repeating <c>tx-resource</c>, are left alone.
        /// </summary>
        /// <exception cref="FhirOperationException"></exception>
        internal Parameters NoDuplicates(IReadOnlyCollection<string> singleValued)
        {
            if (parameters.TryGetDuplicates(out var duplicates) && duplicates.Where(singleValued.Contains).ToList() is { Count: > 0 } invalid)
            {
                var message =
                    $"List of input parameters contains the following duplicates: {string.Join(", ", invalid)}";
                throw FhirOperationException.InvalidOperationInvocation(message);
            }

            return parameters;
        }
    }
}