/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath.Expressions
{
    internal static class ClosureExtensions
    {
        public static IEnumerable<ITypedElement> GetThis(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.this");
        }


        public static void SetThis(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("builtin.this", value);
        }

        public static IEnumerable<ITypedElement> GetThat(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.that");
        }

        public static void SetThat(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("builtin.that", value);
        }

        public static void SetOriginalContext(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("context", value);
        }

        public static IEnumerable<ITypedElement> GetOriginalContext(this Closure ctx)
        {
            return ctx.ResolveValue("context");
        }

        public static IEnumerable<ITypedElement> GetResource(this Closure ctx)
        {
            return ctx.ResolveValue("resource");
        }

        public static void SetResource(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("resource", value);
        }

        public static Closure Nest(this Closure ctx, IEnumerable<ITypedElement> input)
        {
            var nested = ctx.Nest();
            nested.SetThat(input);

            return nested;
        }
    }
}
