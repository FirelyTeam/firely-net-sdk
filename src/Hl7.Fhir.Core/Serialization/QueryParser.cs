/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    internal static class QueryParser
    {
        internal static Query Load (String resource, IEnumerable<Tuple<String, String>> parameters)
        {
            Query result = new Query();
            result.ResourceType = resource;
            foreach (var p in parameters)
            {
                result.AddParameter(p.Item1, p.Item2);
            };
            return result;
        }
    }
}
