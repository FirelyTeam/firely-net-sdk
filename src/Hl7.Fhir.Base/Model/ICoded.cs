/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Marks a resource that is coded.
    /// </summary>
    public interface ICoded
    {
        IEnumerable<Coding> ToCodings();
    }

    /// <summary>
    /// Represents a resource that can be coded.
    /// </summary>
    /// <typeparam name="T">The type that is used to codify the resource, usually a (list of) <see cref="Coding"/> or <see cref="CodeableConcept"/>.</typeparam>
    public interface ICoded<T> : ICoded
    {
        T Code { get; set; }
    }


    /// <summary>
    /// Helper methods for working with coded types.
    /// </summary>
    public static class CodedExtensions
    {
        /// <summary>
        /// Maps a list of FHIR datatypes to a list of <see cref="Coding"/>. See <see cref="ToCodings(DataType)"/> for more details.
        /// </summary>
        /// <exception cref="NotSupportedException">When the datatype is not bindeable, and thus not convertable to a Coding.</exception>
        public static IEnumerable<Coding> ToCodings(this IEnumerable<DataType>? dts) => dts?.SelectMany(dt => dt.ToCodings()) ?? [];

        /// <summary>
        /// Maps a FHIR datatype to a (list of) Coding, according to https://hl7.org/fhir/terminologies.html#4.1
        /// </summary>
        /// <exception cref="NotSupportedException">When the datatype is not bindeable, and thus not convertable to a Coding.</exception>
        public static IEnumerable<Coding> ToCodings(this DataType? dt) => dt switch
        {
            null => Enumerable.Empty<Coding>(),
            Code co => new[] { new Coding(null, co.Value) },
            ISystemAndCode sac => new[] { new Coding(sac.System, sac.Code) },
            Coding cd => new[] { cd },
            CodeableConcept cc => cc.Coding ?? Enumerable.Empty<Coding>(),
            Quantity q => new[] { new Coding(q.System, q.Code) },
            FhirString fs => new[] { new Coding(null, fs.Value) },
            FhirUri u => new[] { new Coding(null, u.Value) },
            CodeableReference { Concept: {} crc } => crc.Coding ?? Enumerable.Empty<Coding>(),
            _ => []
        };
    }
}

#nullable restore