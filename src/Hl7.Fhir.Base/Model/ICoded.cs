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


    public static class CodedExtensions
    {
        public static IEnumerable<Coding> ToCodings(this IEnumerable<DataType> dts) => dts.SelectMany(dt => dt.ToCodings());

        public static IEnumerable<Coding> ToCodings(this DataType dt) => dt switch
        {
            Code co => new[] { new Coding(null, co.Value) },
            ISystemAndCode sac => new[] { new Coding(sac.System, sac.Code) },
            Coding cd => new[] { cd },
            CodeableConcept cc => cc.Coding ?? Enumerable.Empty<Coding>(),
            FhirString fs => new[] { new Coding(null, fs.Value) },
            var unk => throw new NotSupportedException($"Don't know how to derive a list of codings from type '{unk.GetType()}'.")
        };
    }
}

#nullable restore