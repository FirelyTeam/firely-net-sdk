/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System.Collections.Generic;

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

        public static IEnumerable<ITypedElement> GetTotal(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.total");
        }


        public static void SetTotal(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("builtin.total", value);
        }


        public static IEnumerable<ITypedElement> GetThat(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.that");
        }

        public static void SetThat(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("builtin.that", value);
        }

        /// <summary>
        /// The original node that was passed to the evaluation engine before starting evaluation.
        /// </summary>
        public static void SetOriginalContext(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("context", value);
        }

        /// <summary>
        /// The original resource current context is part of. When evaluating a datatype, this would be the
        /// resource the element is part of. Do not go past a root resource into a bundle, if it is contained
        /// in a bundle.
        /// </summary>
        public static void SetResource(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("resource", value);
        }

        /// <summary>
        /// When a DomainResource contains another resource, and that contained resource is the focus (%resource) 
        /// then %rootResource refers to the container resource.
        /// </summary>
        public static void SetRootResource(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("rootResource", value);
        }


        public static IEnumerable<ITypedElement> GetOriginalContext(this Closure ctx)
        {
            return ctx.ResolveValue("context");
        }

        public static IEnumerable<ITypedElement> GetResource(this Closure ctx)
        {
            return ctx.ResolveValue("resource");
        }


        public static IEnumerable<ITypedElement> GetRootResource(this Closure ctx)
        {
            return ctx.ResolveValue("rootResource");
        }

        public static Closure Nest(this Closure ctx, IEnumerable<ITypedElement> input)
        {
            var nested = ctx.Nest();
            nested.SetThat(input);

            return nested;
        }

        public static void SetIndex(this Closure ctx, IEnumerable<ITypedElement> value)
        {
            ctx.SetValue("builtin.index", value);
        }

        public static IEnumerable<ITypedElement> GetIndex(this Closure ctx)
        {
            return ctx.ResolveValue("builtin.index");
        }
    }
}
