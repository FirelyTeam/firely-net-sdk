/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.FluentPath.Support;
using Hl7.ElementModel;

namespace Hl7.FluentPath.Expressions
{ 
    internal static class ClosureExtensions
    {
        public static IEnumerable<IValueProvider> GetThis(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.this");
        }


        public static void SetThis(this Closure ctx, IEnumerable<IValueProvider> value)
        {
            ctx.SetValue("builtin.this", value);
        }

        public static IEnumerable<IValueProvider> GetThat(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.that");
        }

        public static void SetThat(this Closure ctx, IEnumerable<IValueProvider> value)
        {
            ctx.SetValue("builtin.that", value);
        }

        public static void SetOriginalContext(this Closure ctx, IEnumerable<IValueProvider> value)
        {
            ctx.SetValue("context", value);
        }

        public static IEnumerable<IValueProvider> GetOriginalContext(this Closure ctx)
        {
            return ctx.ResolveValue("context");
        }

        public static IEnumerable<IValueProvider> GetResource(this Closure ctx)
        {
            return ctx.ResolveValue("resource");
        }

        public static void SetResource(this Closure ctx, IEnumerable<IValueProvider> value)
        {
            ctx.SetValue("resource", value);
        }

        public static Closure Nest(this Closure ctx, IEnumerable<IValueProvider> input)
        {
            var nested = ctx.Nest();
            nested.SetThat(input);

            return nested;
        }
    }
}
