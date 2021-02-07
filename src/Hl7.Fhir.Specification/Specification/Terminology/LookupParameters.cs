/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LookupParameters
    {
        /// <summary>
        /// The code that is to be located. If a code is provided, a system must be provided.
        /// </summary>
        public Code Code { get; private set; }
        /// <summary>
        /// The system for the code that is to be located.
        /// </summary>
        public FhirUri System { get; private set; }
        /// <summary>
        /// The version of the system, if one was provided in the source data.
        /// </summary>
        public FhirString Version { get; private set; }
        /// <summary>
        /// A coding to look up.
        /// </summary>
        public Coding Coding { get; private set; }
        /// <summary>
        /// The date for which the information should be returned.
        /// </summary>
        public FhirDateTime Date { get; private set; }
        /// <summary>
        /// The requested language for display.
        /// </summary>
        public Code DisplayLanguage { get; private set; }
        /// <summary>
        /// A property that the client wishes to be returned in the output.
        /// </summary>
        /// <remarks>If no properties are specified, the server chooses what to return.</remarks>
        public IEnumerable<Code> Property { get; private set; }

        #region Builder methods
        public LookupParameters WithCode(string code = null, string system = null, string version = null, string displayLanguage = null)
        {
            if (!string.IsNullOrWhiteSpace(code)) Code = new Code(code);
            if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
            if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);
            if (!string.IsNullOrWhiteSpace(displayLanguage)) DisplayLanguage = new Code(displayLanguage);
            return this;
        }

        public LookupParameters WithDate(FhirDateTime date)
        {
            Date = date;
            return this;
        }

        public LookupParameters WithProperties(string[] properties)
        {
            Property = properties?.Select(p => new Code(p));
            return this;
        }
        #endregion


        public Parameters Build()
        {
            var result = new Parameters();

            if (Code is { }) result.Add("code", Code);
            if (System is { }) result.Add("system", System);
            if (Version is { }) result.Add("version", Version);
            if (Coding is { }) result.Add("coding", Coding);
            if (Date is { }) result.Add("date", Date);
            if (DisplayLanguage is { }) result.Add("displayLanguage", DisplayLanguage);

            foreach (var prop in Property ?? Enumerable.Empty<Code>())
            {
                result.Add("property", prop);
            }

            return result;
        }
    }
}
