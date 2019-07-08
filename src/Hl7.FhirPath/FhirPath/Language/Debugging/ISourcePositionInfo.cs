/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Language.Debugging
{
    public interface ISourcePositionInfo : IPositionInfo
    {
        Source Source { get; }
    }


    public static class PositionInfoExtensions
    {
        private static readonly Random _generator = new Random((int)DateTimeOffset.Now.UtcTicks);

        public static StackFrame ToStackFrame(this ISourcePositionInfo location, string name = null) =>
            new StackFrame
            {

                Id = _generator.Next(),
                Column = location.LinePosition,
                Line = location.LineNumber,
                Name = name,
                Source = location.Source
            };

        public static string GetLocationLabel(this ISourcePositionInfo loc)
        {
            if (loc == null) throw new ArgumentNullException(nameof(loc));

            var result = $"line {loc.LineNumber}";
            if (loc.LinePosition > 0) result += $", column {loc.LinePosition}";
            return result;
        }
    }
}
