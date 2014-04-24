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
