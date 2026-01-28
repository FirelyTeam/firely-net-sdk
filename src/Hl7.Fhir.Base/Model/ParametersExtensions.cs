/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using System.Collections.Generic;
using System.Linq;


namespace Hl7.Fhir.Model;

/// <summary>
/// Extension methods for the Parameters class.
/// </summary>
public static class ParametersExtensions
{
    private const string CODEATTRIBUTE = "code";
    private const string URLATTRIBUTE = "url";
    private const string CONTEXTATTRIBUTE = "context";
    private const string VALUESETATTRIBUTE = "valueSet";

    extension(Parameters parameters)
    {
        /// <summary>
        /// Attempts to find duplicate parameter names in the Parameters.
        /// </summary>
        /// <param name="duplicates">Output parameter containing duplicate names.</param>
        /// <returns>True if duplicates are found, false otherwise.</returns>
        public bool TryGetDuplicates(out IEnumerable<string> duplicates)
        {
            duplicates = parameters.Parameter.Select(p => p.Name)
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key!)
                .ToList();

            return duplicates.Any();
        }

        /// <summary>
        /// Checks if the Parameters contains a parameter with the specified name.
        /// </summary>
        /// <param name="name">The parameter name to look for.</param>
        /// <returns>True if the parameter exists, false otherwise.</returns>
        public bool HasParam(string name) => parameters.Parameter.Any(p => p.Name == name);
    }
}