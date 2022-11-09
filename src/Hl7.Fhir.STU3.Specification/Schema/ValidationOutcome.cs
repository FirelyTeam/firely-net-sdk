/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal enum ValidationResult
    {
        Valid,
        Invalid,
        Undecided
    }


    internal enum Weight
    {
        Information,
        Note,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// All semantics about what is wrong goes here
    /// </summary>
    internal class ValidationDetail
    {
        public readonly int Code;
        public readonly Weight WeightIndication;
        public readonly string Diagnostics;     // low level diag

        public ValidationDetail(int code, Weight weight, string dx=null)
        {
            Code = code;
            WeightIndication = weight;
            Diagnostics = dx;
        }

        public static ReadOnlyCollection<ValidationDetail> Single(int code, Weight weight, string dx = null) =>
            Array.AsReadOnly(new[] { new ValidationDetail(code, weight, dx) });
    }

    internal class ValidationOutcome
    {
        public readonly ValidationResult Result;
        public readonly ITypedElement Focus;
        public readonly IAssertion Source;

        public ICollection<ValidationDetail> Details;
        public ICollection<ValidationOutcome> Nested;


        public ValidationOutcome(IAssertion source, ValidationResult result, ITypedElement focus,
            IEnumerable<ValidationDetail> details=null, IEnumerable<ValidationOutcome> nested = null) 
        {
            Source = source;
            Result = result;
            Focus = focus;
            Details = details?.Any() == true ? new List<ValidationDetail>(details) : null;
            Nested = nested?.Any() == true ? new List<ValidationOutcome>(nested) : null;
        }
    }   
}
