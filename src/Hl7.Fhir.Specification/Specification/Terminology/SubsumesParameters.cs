/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Terminology
{
    public class SubsumesParameters
    {
        /// <summary>
        /// The "A" code that is to be tested. If a code is provided, a system must be provided.
        /// </summary>
        public Code CodeA { get; private set; }
        /// <summary>
        /// The "B" code that is to be tested. If a code is provided, a system must be provided.
        /// </summary>
        public Code CodeB { get; private set; }
        /// <summary>
        /// The code system in which subsumption testing is to be performed. 
        /// This must be provided unless the operation is invoked on a code system instance.
        /// </summary>
        public FhirUri System { get; private set; }
        /// <summary>
        /// The version of the code system, if one was provided in the source data.
        /// </summary>
        public FhirString Version { get; private set; }
        /// <summary>
        /// The "A" Coding that is to be tested.
        /// </summary>
        public Coding CodingA { get; private set; }
        /// <summary>
        /// The "B" Coding that is to be tested.
        /// </summary>
        public Coding CodingB { get; private set; }

        #region Build methods
        public SubsumesParameters WithCode(string codeA, string codeB, string system = null, string version = null)
        {
            if (!string.IsNullOrWhiteSpace(codeA)) CodeA = new Code(codeA);
            if (!string.IsNullOrWhiteSpace(codeB)) CodeB = new Code(codeB);
            if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
            if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);

            return this;
        }

        public SubsumesParameters WithCoding(Coding codingA, Coding codingB, string system = null, string version = null)
        {
            CodingA = codingA;
            CodingB = codingB;
            if (!string.IsNullOrWhiteSpace(system)) System = new FhirUri(system);
            if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);

            return this;
        }
        #endregion

        public Parameters Build()
        {
            var result = new Parameters();

            if (CodeA is { }) result.Add("codeA", CodeA);
            if (CodeB is { }) result.Add("codeB", CodeB);
            if (System is { }) result.Add("system", System);
            if (Version is { }) result.Add("version", Version);
            if (CodingA is { }) result.Add("codingA", CodingA);
            if (CodingB is { }) result.Add("codingB", CodingB);

            return result;
        }
    }
}
