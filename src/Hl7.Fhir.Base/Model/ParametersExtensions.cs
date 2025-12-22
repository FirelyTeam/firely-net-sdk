/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Rest;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace Hl7.Fhir.Model;

public static class ParametersExtensions
{
    private const string CODEATTRIBUTE = "code";
    private const string URLATTRIBUTE = "url";
    private const string CONTEXTATTRIBUTE = "context";
    private const string VALUESETATTRIBUTE = "valueSet";

    extension(Parameters parameters)
    {
        public bool TryGetDuplicates(out IEnumerable<string> duplicates)
        {
            duplicates = parameters.Parameter.Select(p => p.Name)
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key!)
                .ToList();

            return duplicates.Any();
        }

        public bool HasParam(string name) => parameters.Parameter.Any(p => p.Name == name);
    }
}