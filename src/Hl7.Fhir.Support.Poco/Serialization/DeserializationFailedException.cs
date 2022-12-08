/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Contains the list of errors detected while deserializing data into .NET POCOs.
    /// </summary>
    /// <remarks>The deserializers will continue deserialization in the face of errors, and so will collect the full
    /// set of errors detected using this aggregate exception.</remarks>
    public class DeserializationFailedException : Exception
    {
        public DeserializationFailedException(Base? partialResult, IEnumerable<CodedException> innerExceptions) :
            base(generateMessage(innerExceptions))
        {
            PartialResult = partialResult;
            Exceptions = innerExceptions.ToList();
        }

        private static string generateMessage(IEnumerable<CodedException> exceptions)
        {
            string b = "One or more errors occurred.";
            if (exceptions.Any())
                b += " " + string.Join(" ", exceptions.Select(e => $"({e.Message})"));

            return b;
        }


        /// <summary>
        /// The best-effort result of deserialization. Maybe invalid or incomplete because of the errors encountered.
        /// </summary>
        public Base? PartialResult { get; private set; }

        public IReadOnlyCollection<CodedException> Exceptions { get; }
    }
}

#nullable restore
