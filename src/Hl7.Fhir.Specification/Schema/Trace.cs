/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation.Schema;
using Newtonsoft.Json.Linq;
using System;
using Issue = Hl7.Fhir.Support.Issue;

namespace Hl7.Fhir.Specification.Schema
{
    internal class Trace : IAssertion
    {
        public readonly string Message;
        public readonly Issue Details;

        public Trace(string message, Issue details = null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Details = details;
        }

        public JToken ToJson()
        {
            var trace = new JObject(new JProperty("message", Message));

            if (Details != null)
            {
                trace.Add(new JProperty("code", Details.Code));
                trace.Add(new JProperty("severity", Details.Severity.GetLiteral()));
                trace.Add(new JProperty("category", Details.Type.GetLiteral()));
            }

            return new JProperty("trace", trace);
        }

    }
}